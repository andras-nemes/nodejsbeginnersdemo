var customerService = require('./customerService');

module.exports.getAllCustomers = function (next) {
    customerService.getAllCustomers(function (err, res) {
        if (err) {
            next(err, null);
        }
        else {
            next(null, res);
        }
    });
};

module.exports.getCustomerById = function (id, next) {
    customerService.getCustomerById(id, function (err, res) {
        if (err) {
            next(err, null);
        }
        else {
            next(null, res);
        }
    });
};

module.exports.insertNewCustomer = function (customerName, next) {
    customerService.insertNewCustomer(customerName, function (err, res) {
        if (err) {
            next(err, null);
        }
        else {
            next(null, res);
        }
    });
};

module.exports.addOrders = function (customerId, orderItems, next) {
    customerService.addOrders(customerId, orderItems, function (err, res) {
        if (err) {
            next(err, null);
        }
        else {
            next(null, res);
        }
    });
};

module.exports.deleteCustomer = function (customerId, next) {
    customerService.deleteCustomer(customerId, function (err, res) {
        if (err) {
            next(err, null);
        }
        else {
            next(null, res);
        }
    });
};