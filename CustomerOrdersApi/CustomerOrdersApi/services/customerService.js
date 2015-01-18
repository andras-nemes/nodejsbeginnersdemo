var customerRepository = require('../repositories/customerRepository');

module.exports.getAllCustomers = function (next) {
    customerRepository.getAll(function (err, res) {
        if (err) {
            next(err, null);
        }
        else {
            next(null, res);
        }
    });
};

module.exports.getCustomerById = function (customerId, next) {
    customerRepository.getById(customerId, function (err, res) {
        if (err) {
            next(err, null);
        }
        else {
            next(null, res);
        }
    });
};

module.exports.addOrders = function (customerId, orderItems, next) {
    if (!customerId) {
        var err = "Missing customer id property";
        next(err, null);
    }
    else {
        customerRepository.addOrders(customerId, orderItems, function (err, res) {
            if (err) {
                next(err, null);
            }
            else {
                next(null, res);
            }
        });
    }
};

module.exports.deleteCustomer = function (customerId, next) {
    if (!customerId) {
        var err = "Missing customer id property";
        next(err, null);
    }
    else {
        customerRepository.remove(customerId, function (err, res) {
            if (err) {
                next(err, null);
            }
            else {
                next(null, res);
            }
        });
    }
};

module.exports.insertNewCustomer = function (customerName, next) {
    if (!customerName) {
        var err = "Missing customer name property";
        next(err, null);
    }
    else {
        customerRepository.insertBrandNew(customerName, function (err, res) {
            if (err) {
                next(err, null);
            }
            else {
                next(null, res);
            }
        });
    }
};