var customerController = require('./customersController');

module.exports.start = function (app) {
    customerController.start(app);
};