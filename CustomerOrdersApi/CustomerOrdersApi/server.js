var http = require('http');
var express = require('express');
var controllers = require('./controllers');
var bodyParser = require('body-parser')

var app = express();
app.use(bodyParser.urlencoded({ extended: false }))
app.use(bodyParser.json())

controllers.start(app);

var port = process.env.port || 1337;
http.createServer(app).listen(port);

/*
 * Example module code
 * 
var exampleModule = require('./modules/module_example.js');
var ex = new exampleModule();
var greeting = ex.greeting;

exampleModule.defaultGreeting;

var ret = exampleModule.greatFunction();*/