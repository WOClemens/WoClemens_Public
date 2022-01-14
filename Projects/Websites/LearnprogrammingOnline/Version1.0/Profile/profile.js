function submitProfile(){
	var xmlhttp = new XMLHttpRequest();   // new HttpRequest instance
    var theUrl = "http://localhost:3000/profile";

	var name = document.getElementById("pname").value;
	var passwort = document.getElementById("ppasswort").value;


	xmlhttp.open("POST", theUrl);
  xmlhttp.setRequestHeader("Content-Type", "application/json;charset=UTF-8");
	xmlhttp.send(JSON.stringify({
    "pName": name,
    "pPasswort": passwort
	}));

}

var proName;
var proPasswort;

function login(){
  var request = new XMLHttpRequest();
  request.open('GET', 'http://localhost:3000/profile', true);

  var name = document.getElementById("pname").value;
	var passwort = document.getElementById("ppasswort").value;
  request.onload = function () {
    // Begin accessing JSON data here
    var data = JSON.parse(this.response);
    if (request.status >= 200 && request.status < 400) {
      data.forEach(profil => {
        var inName = profil.pName;
        var inPasswort = profil.pPasswort;

        if(name == inName)
        {
          if(passwort == inPasswort)
          {
            loadPage();
            proName = profil.pName;
            proPasswort = profil.pPasswort;
          }
        }


          });
        }
      }
      request.send();
}

function register(){

  var xmlhttp = new XMLHttpRequest();   // new HttpRequest instance
  var theUrl = "http://localhost:3000/profile";

	var name = document.getElementById("pname").value;
	var passwort = document.getElementById("ppasswort").value;

	xmlhttp.open("POST", theUrl);
  xmlhttp.setRequestHeader("Content-Type", "application/json;charset=UTF-8");
	xmlhttp.send(JSON.stringify({
    "pName": name,
    "pPasswort": passwort
	}));

}

function loadPage(){
	window.location = "index.html";
}
