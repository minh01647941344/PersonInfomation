ko.extenders.logChange = function (target, option) {
    target.hasError = ko.observable(false);
    target.validationMessage = ko.observable("");
    function validate(newValue) {
        if (option == 'fName') {
            if (newValue.charAt(0) == 'M') {
                if (newValue.length < 5 || newValue.length > 10) {
                    target.hasError(true);
                    target.validationMessage("Length is 5 - 10 char");
                }else{
                    target.hasError(false);
                    target.validationMessage("");
                }
            } else {
                target.hasError(true);
                target.validationMessage("The first name start with 'M'");
            }
        }

        if (option == 'lName') {
            if (newValue.length < 5 || newValue.length > 10) {
                target.hasError(true);
                target.validationMessage("Length is 5 - 10 char");
            } else {
                target.hasError(false);
                target.validationMessage("");
            }
        }

        if (option == 'age') {
            let number = 0 + newValue;
            if (parseInt(newValue) < 1 || parseInt(newValue) > 99 || number == "0") {
                target.hasError(true);
                target.validationMessage("The age is between 1 - 99");
            } else {
                target.hasError(false);
                target.validationMessage("");
            }
        }
    }


    validate(target());

    target.subscribe(validate);

    return target;
}
//Fake constructor
function createUser(firstName, lastName, age) {
    this.firstName = ko.observable(firstName).extend({ logChange: "fName" });
    this.lastName = ko.observable(lastName).extend({ logChange: "lName" });
    this.age = ko.observable(age).extend({ logChange: "age" });
}
//Get Data by Ajax

var filterArray = function () {
    var self = this;
    var xhttp;
    this.filterArray = [];

    //create xmlhttpRequest
    if (window.XMLHttpRequest) {
        xhttp = new XMLHttpRequest();
    } else {
        xhttp = new ActiveXObject("Microsoft.XMLHTTP");
    }
    xhttp.onreadystatechange = function () {       
        //check code connect
        if (this.readyState == 4 && this.status == 200) {
            var x = this.responseText;
            //parse responseText to JSON
            x = JSON.parse(x.replace('<?xml version="1.0" encoding="utf-8"?>', '').replace('<string xmlns="http://tempuri.org/">', '').replace('</string>',''));
            //Check key of JSON file
            if (!x.firstName) {
                for (key in x) {
                    self.filterArray.push(new createUser(x[key].firstName, x[key].lastName, x[key].age));
                }
            } else {
                self.filterArray.push(new createUser(x.firstName, x.lastName, x.age));
            }
        }
    };
    //Open and get data from backend
    xhttp.open("POST", "/BLL/PersonFunny.asmx/getPersonList", true);
    xhttp.send();

    return this.filterArray;
}

//Sum Age Available
var totalAge = function (array) {
    var sumValue = 0;
    array().forEach(item => {
        if (item.age()) {
            sumValue += parseInt(item.age());
        }
    });
    return sumValue;
}
//Check position
var position = function (removeList, object) {
    let position = removeList.findIndex(function (item) {
        if (item.firstName == object.firstName && item.lastName == object.lastName && item.age == object.age) {
            return true;
        }
    });
    return position;
}
//Push or Remove observableArray
function exChange(removeList, pushList, object) {
    removeList.remove(object);
    pushList.push(object);
}
//return rank of user
var rank = function (age) {
    var rankUser = " ";
    if (age > 62) {
        rankUser = "Retired";
    }
    if (age < 15) {
        rankUser = "Teenage";
    }
    if (age >= 15 && age <= 62) {
        rankUser = "Adult";
    }
    return rankUser;
}

//View Model
var ViewModel = {
    //Declare
    userCondition: ko.observableArray(filterArray()),
    removeUserList: ko.observableArray([]),
    selectedItem: ko.observable(new createUser('Choose user!', 'Choose user!', 0)),

    //sum age of Available User List
    sumAgeAvailableList: ko.pureComputed({
        read: function () {
            return totalAge(ViewModel.userCondition);
        },
        write: function (value) {
            return value;
        },
        owner: this
    }),

    ////sum age of Available User List
    sumAgeRemoveList: ko.pureComputed({
        read: function () {
            return totalAge(ViewModel.removeUserList);
        },
        write: function (value) {
            return value;
        },
        owner: this
    }),
    //show full name
    fullName: ko.pureComputed({
        read: function () {
            if (ViewModel.selectedItem().firstName() == 'Choose user!') {
                return "No infomation";
            } else {
                return ViewModel.selectedItem().firstName() + " " + ViewModel.selectedItem().lastName() + " is " + rank(ViewModel.selectedItem().age());
            }
        },
        write: function (value) {
            return value;
        },
        owner: this
    }),
    //feature of list
    removeUser: function (item) {
        exChange(ViewModel.userCondition, ViewModel.removeUserList, item);
    },
    undoUser: function (item) {
        exChange(ViewModel.removeUserList, ViewModel.userCondition, item);
    },
    selectUser: function (item) {
        ViewModel.selectedItem(item);
        ViewModel.fullName(ViewModel.selectedItem().firstName + " " + ViewModel.selectedItem().lastName + " is " + rank(ViewModel.selectedItem().age()));
    }
}