var app = new Vue({
    el: '#taxCalculator',
    data: {
        postalCode: "",
        annualIncome: 0,
        postalCodes: [],
        taxCalculationType: "",
        taxAmountPayable: 0
    },
    methods: {
        calculateTax: () => {
            calculateTax();
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
            postalCode: app.postalCode,
            annualIncome: app.annualIncome
        })
        .then((res) => {
            app.taxCalculationType = res.data.taxCalculationType;
            app.taxAmountPayable = res.data.taxAmountPayable;
        });
}

(function onLoad() {
    getPostalCodes();
})();