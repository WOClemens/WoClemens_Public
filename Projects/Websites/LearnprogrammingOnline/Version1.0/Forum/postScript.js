function submitToServer(){
	var xmlhttp = new XMLHttpRequest();   // new HttpRequest instance
    var theUrl = "http://localhost:3000/fragen";

	var type = document.getElementById("fType").value;
	var titel = document.getElementById("fTitle").value;
	var description = document.getElementById("fDescription").value;
	var name = document.getElementById("fName").value;


	xmlhttp.open("POST", theUrl);
  xmlhttp.setRequestHeader("Content-Type", "application/json;charset=UTF-8");
	xmlhttp.send(JSON.stringify({
    "type": type,
    "tital": titel,
    "description": description,
    "name": name
	}));

}

function loadPage(){
	window.location = "index.html";
}
