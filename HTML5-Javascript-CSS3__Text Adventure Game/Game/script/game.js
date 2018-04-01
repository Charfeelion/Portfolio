//
//
//
//
//
//
//
//
//
//

//---Globals---\\
let view = document.getElementById("game");
let enter = document.getElementById("play");
let save = document.getElementById("save");
let load = document.getElementById("load");
let userInput = document.getElementById("input");
let feedback = document.getElementById("feedback");
let newGame = document.getElementById("gameStart");
let story = document.getElementById("story");
let learnDisplay = document.getElementById("learnDisplay");

//---Objects---\\
var player = {
    commands: ["left", "right", "scale in", "scale out", "learn", "create"],
    learned: [],
    currentRoom: "",
    commandCompare: function (compare) {    //compares userInput to commands
        let flag = false;
        console.log(compare == this.commands[0]);
        for (var i = 0; i < this.commands.length; i++) {
            if (compare == this.commands[i]) {
                flag = true;    //comparison has been found!
                break;
            }
        }
        return flag;
    }

}

var map_universe = {
    image: "url('images/universe.png') no-repeat center",
    text: "You are currently observing our universe. Some say it is infinite, while others say it has a boundary...",
    learn: function (){
        player.learned.push("universe");
    },
    left: function(){
        feedback.innerText = "You cannot proceed this way.";
    },
    right: function(){
        feedback.innerText = "You cannot proceed this way.";
    },
    scaleIn: function () {
        player.currentRoom = "map_supercluster";
    },
    scaleOut: function(){

    }
    
}

var map_supercluster = {
    image: "url('images/supercluster.jpg') no-repeat center",
    text: "This is our home supercluster known as Laniakea",
    learn: function () {
        if (player.learned.indexOf("supercluster") > 0) { //if there is an index, for found item
            //do nothing
            console.log(player.learned.indexOf("supercluster"));
        }
        else {
            //otherwise add it to learned
            console.log("SOMETHING");
            player.learned.push("supercluster");
        }
        learnDisplay.innerText = "We don't know much about it, other than it's shape and contents. It is";
    },
    left: function () {
        feedback.innerText = "You cannot proceed this way.";
    },
    right: function () {
        feedback.innerText = "You cannot proceed this way.";
    },
    scaleIn: function () {
        player.currentRoom = "map_localGroup";
    },
    scaleOut: function () {
        player.currentRoom = "map_universe";
    }
}

var map_localGroup = {
    image: "url('images/localGroup.png') no-repeat center",
    text: "This is our local group, home to over 50 galaxies",
    learn: function () {
        if (player.learned.indexOf("supercluster") > 0) { //if there is an index, for found item
            //do nothing
            console.log(player.learned.indexOf("supercluster"));
        }
        else {
            //otherwise add it to learned
            console.log("SOMETHING");
            player.learned.push("supercluster");
        }
        learnDisplay.innerText = "We don't know much about it, other than it's shape and contents. It is";
    },
    left: function () {
        feedback.innerText = "You cannot proceed this way.";
    },
    right: function () {
        feedback.innerText = "You cannot proceed this way.";
    },
    scaleIn: function () {

    },
    scaleOut: function () {
        player.currentRoom = "map_supercluster";
    }
}

var map_milkyway = {
    image: "url('images/localGroup.png') no-repeat center",
    text: "This is our local group, home to over 50 galaxies",
    learn: function () {
        if (player.learned.indexOf("supercluster") > 0) { //if there is an index, for found item
            //do nothing
            console.log(player.learned.indexOf("supercluster"));
        }
        else {
            //otherwise add it to learned
            console.log("SOMETHING");
            player.learned.push("supercluster");
        }
        learnDisplay.innerText = "We don't know much about it, other than it's shape and contents. It is";
    },
    left: function () {
        feedback.innerText = "You cannot proceed this way.";
    },
    right: function () {
        feedback.innerText = "You cannot proceed this way.";
    },
    scaleIn: function () {

    },
    scaleOut: function () {
        player.currentRoom = "map_supercluster";
    }
}

var map_solarSystem = {
    image: "url('images/localGroup.png') no-repeat center",
    text: "This is our local group, home to over 50 galaxies",
    learn: function () {
        if (player.learned.indexOf("supercluster") > 0) { //if there is an index, for found item
            //do nothing
            console.log(player.learned.indexOf("supercluster"));
        }
        else {
            //otherwise add it to learned
            console.log("SOMETHING");
            player.learned.push("supercluster");
        }
        learnDisplay.innerText = "We don't know much about it, other than it's shape and contents. It is";
    },
    left: function () {
        feedback.innerText = "You cannot proceed this way.";
    },
    right: function () {
        feedback.innerText = "You cannot proceed this way.";
    },
    scaleIn: function () {

    },
    scaleOut: function () {
        player.currentRoom = "map_supercluster";
    }
}

//---Functions---\\

function render() {
    let input = userInput.value;
    if (player.commandCompare(input)) {
        switch (userInput.value) {
            case "left":
                eval(player.currentRoom).left();
                break;
            case "right":
                break;
            case "scale in":
                eval(player.currentRoom).scaleIn();
                break;
            case "scale out":
                eval(player.currentRoom).scaleOut();
                break;
            case "learn":
                eval(player.currentRoom).learn();
                break;
            case "create":
                break;
        }
    }
    userInput.value = "";   //
    story.innerHTML = "";   //reset
    story.innerText = eval(player.currentRoom).text;
    view.style.background = eval(player.currentRoom).image;
}

function saveGame() {
    if (confirm("Warning! Saving will overwrite data. Continue?")) {
        localStorage.clear();
        localStorage.setItem("learned", JSON.stringify(player.learned));    //save learned items into array
        localStorage.setItem("map", player.currentRoom);
    }
    else {
        //do nothing
    }
}

function loadGame() {
    player.learned = JSON.parse(localStorage.getItem("learned"));
    player.currentRoom = localStorage.getItem("map");

    userInput.value = "";
    render();
}

function clearSave() {
    localStorage.clear();
}

function start() {
    player.currentRoom = "map_universe";
    userInput.value = "";
    render();
}

function keyHandler(event) {
    if (event.keyCode == 13) {
        render();
    }
}

//---Events---\\
var room = eval(player.currentRoom);
enter.addEventListener("click", render, false);
window.addEventListener("keydown", keyHandler, false);
save.addEventListener("click", saveGame, false);
load.addEventListener("click", loadGame, false);
clear.addEventListener("click", clearSave, false);
newGame.addEventListener("click", start, false);