var app = new Vue({
    el: '#taxCalculator',
    data: {
        errorMessage: "",
        requests: {
            postalCode: "",
            annualIncome: ""
        },
        postalCodes: [{
            postalCode: ""
        }],
        submitted: false,
        viewHistory: false,
        calculatedTax: {
            taxCalculationType: "",
            taxAmountPayable: 0
        },
        transactionHistory: [{
            annualIncome: 0,
            postalCode: "",
            calculatedTax: 0,
            calculationType: "",
            createdOnDateString: ""
        }]
    },
    methods: {
        calculateTax: () => {
            calculateTax();
        },
        loadTransactionHistory: () => {
            loadTransactionHistory();
        }
    }
});

function getPostalCodes() {
    axios.get('/api/postalcode')
        .then((res) => {
            app.postalCodes = res.data.payload;
        })
        .catch((err) => {
            if (err.response.data.error) {
                app.errorMessage = err.response.data.error.message;
            }
            else {
                app.errorMessage = "Could not process the request. Please contact service support on xxx-xxxx-xx"
            }
        });
}

function calculateTax() {
    if (isNaN(app.requests.annualIncome)) {
        app.errorMessage = "Annual income needs to be a numeric value";
        return;
    }
    if (!app.requests.annualIncome || !app.requests.postalCode) {
        app.errorMessage = "Both annual income and postal code needs to be filled in";
        return;
    }

    axios.post('/api/taxcalculator',
        {
            postalCode: app.requests.postalCode,
            annualIncome: app.requests.annualIncome
        })
        .then((res) => {
            app.calculatedTax.taxCalculationType = res.data.payload.taxCalculationType;
            app.calculatedTax.taxAmountPayable = res.data.payload.taxAmountPayable;
            app.viewHistory = false;
            app.submitted = true;
        })
        .catch((err) => {
            if (err.response.data.error) {
                app.errorMessage = err.response.data.error.message;
            }
            else {
                app.errorMessage = "Could not process the request. Please contact service support on xxx-xxxx-xx"
            }
        });
}

function loadTransactionHistory() {
    axios.get('/api/taxcalculatorhistory')
        .then((res) => {
            app.transactionHistory = res.data.payload;
            app.submitted = false;
            app.viewHistory = true;
        })
        .catch((err) => {
            if (err.response.data.error) {
                app.errorMessage = err.response.data.error.message;
            }
            else {
                app.errorMessage = "Could not process the request. Please contact service support on xxx-xxxx-xx"
            }
        });
}

(function onLoad() {
    getPostalCodes();
})();