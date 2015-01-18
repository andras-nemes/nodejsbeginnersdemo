var mongoDb = require('mongodb');
var connectionString = "mongodb://localhost:27017/customers";
var database = null;

module.exports.getDbHandle = function (next) {
    if (!database) {
        mongoDb.MongoClient.connect(connectionString, function (err, db) {
            if (err) {
                next(err, null);
            }
            else {
                database = db;
                next(null, database);
            }
        });
    }
    else {
        next(null, database);
    }
};