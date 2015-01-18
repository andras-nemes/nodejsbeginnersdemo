var customerService = require('../services');

module.exports.start = function (app) {
    app.get("/customers", function (req, res) {
        
        customerService.getAllCustomers(function (err, customers) {
            if (err) {
                res.status(400).send(err);
            }
            else {
                res.set('Content-Type', 'application/json');
                res.status(200).send(customers);
            }
        });
    });
    
    app.get("/customers/:id", function (req, res) {
        
        var customerId = req.params.id;
        customerService.getCustomerById(customerId, function (err, customer) {
            if (err) {
                res.status(400).send(err);
            }
            else {
                res.set('Content-Type', 'application/json');
                res.status(200).send(customer);
            }
        });
        
    });
    
    app.post("/customers", function (req, res) {
        var customerName = req.body.customerName;
        customerService.insertNewCustomer(customerName, function (err, newCustomer) {
            if (err) {
                res.status(400).send(err);
            }
            else {
                res.set('Content-Type', 'application/json');
                res.status(201).send(newCustomer);
            }
        });

    });
    
    app.put("/customers", function (req, res) {
        var orders = req.body.orders;
        var customerId = req.body.customerId;
        customerService.addOrders(customerId, orders, function (err, itemCount) {
            if (err) {
                res.status(400).send(err);
            }
            else {
                res.set('Content-Type', 'text/plain');
                res.status(200).send(itemCount.toString());
            }
        });
    });

    app.delete("/customers/:id", function (req, res) {
        var customerId = req.params.id;
        customerService.deleteCustomer(customerId, function (err, itemCount) {
            if (err) {
                res.status(400).send(err);
            }
            else {
                res.set('Content-Type', 'text/plain');
                res.status(200).send(itemCount.toString());
            }
        });
    });
};