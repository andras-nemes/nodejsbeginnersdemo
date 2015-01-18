var databaseAccess = require('./access');

module.exports.getAll = function (next) {
    databaseAccess.getDbHandle(function (err, db) {
        if (err) {
            next(err, null);
        }
        else {
            db.collection("customers").find().toArray(function (err, res) {
                if (err) {
                    next(err, null);
                }
                else {
                    next(null, res);
                }
            });
        }
    });
};

module.exports.getById = function (customerId, next) {
    databaseAccess.getDbHandle(function (err, db) {
        if (err) {
            next(err, null);
        }
        else {
            var mongoDb = require('mongodb');
            var BSON = mongoDb.BSONPure;
            var objectId = new BSON.ObjectID(customerId);
            db.collection("customers").find({ '_id': objectId }).toArray(function (err, res) {
                if (err) {
                    next(err, null);
                }
                else {
                    next(null, res);
                }
            });
        }
    });
};

module.exports.addOrders = function (customerId, newOrders, next) {
    databaseAccess.getDbHandle(function (err, db) {
        if (err) {
            next(err, null);
        }
        else {
            var collection = db.collection("customers");
            var mongoDb = require('mongodb');
            var BSON = mongoDb.BSONPure;
            var objectId = new BSON.ObjectID(customerId);
            collection.find({ '_id': objectId }).count(function (err, count) {
                if (count == 0) {
                    err = "No matching customer";
                    next(err, null);
                }
                else {
                    collection.update({ '_id': objectId }, { $addToSet: { orders: { $each: newOrders } } }, function (err, result) {
                        if (err) {
                            next(err, null);
                        }
                        else {
                            next(null, result);
                        }
                    });
                }
            });
        }
    });
};

module.exports.remove = function (customerId, next) {
    databaseAccess.getDbHandle(function (err, db) {
        if (err) {
            next(err, null);
        }
        else {
            var collection = db.collection("customers");
            var mongoDb = require('mongodb');
            var BSON = mongoDb.BSONPure;
            var objectId = new BSON.ObjectID(customerId);
            collection.remove({ '_id': objectId }, function (err, result) {
                if (err) {
                    next(err, null);
                }
                else {
                    next(null, result);
                }
            });
        }
    });
};

module.exports.insertBrandNew = function (customerName, next) {
    databaseAccess.getDbHandle(function (err, db) {
        if (err) {
            next(err, null);
        }
        else {
            //check for existence of customer name
            var collection = db.collection("customers");
            collection.find({ 'name': customerName }).count(function (err, count) {
                if (err) {
                    next(err, null);
                }
                else {
                    if (count > 0) {
                        err = "Customer with this name already exists";
                        next(err, null);
                    }
                    else {
                        //insert new customer with empty orders array
                        var newCustomer = {
                            name : customerName
                            , orders : []
                        };
                        collection.insert(newCustomer, function (err, result) {
                            if (err) {
                                next(err, null);
                            }
                            else {
                                next(null, result);
                            }
                        });                        
                    }
                }
            });
        }
    });
};