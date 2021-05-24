var app = new Vue({
    el: '#taxCalculator',
    data: {
        postalCodes: []
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
            postalCode: "",
            annualIncome: ""
        })
        .then((res) => {
            // res data binding
        });
}

(function onLoad() {
    getPostalCodes();
})();