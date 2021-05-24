var app = new Vue({
    el: '#taxCalculator',
    data: {
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
            app.postalCodes = res.data;
        });
}

function calculateTax() {
    axios.post('/api/taxcalculator',
        {
            postalCode: app.requests.postalCode,
            annualIncome: app.requests.annualIncome
        })
        .then((res) => {
            app.calculatedTax.taxCalculationType = res.data.taxCalculationType;
            app.calculatedTax.taxAmountPayable = res.data.taxAmountPayable;
            app.viewHistory = false;
            app.submitted = true;
        });
}

function loadTransactionHistory() {
    axios.get('/api/taxcalculatorhistory')
        .then((res) => {
            console.log(res);
            app.transactionHistory = res.data;
            app.submitted = false;
            app.viewHistory = true;
        });
}

(function onLoad() {
    getPostalCodes();
})();