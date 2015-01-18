module.exports = function () {
    this.greeting = "Hello from constructor!";
};

module.exports.greatFunction = function ()
{
    return "Greetings from great function!";
}

module.exports.defaultGreeting = "Hello from property";