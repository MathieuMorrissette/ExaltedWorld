function sendHttpGetRequest(theUrl, callback)
{
	var xmlHttp = new XMLHttpRequest();
	xmlHttp.onreadystatechange = function() { 
		if (xmlHttp.readyState == XMLHttpRequest.DONE && xmlHttp.status == 200){
			callback(xmlHttp.responseText);
		}
	}

	xmlHttp.open("GET", theUrl, true); 
	xmlHttp.send(null);
}

// We will always send json
function sendHttpPostRequest(theUrl, data, callback)
{
	var xmlHttp = new XMLHttpRequest();
	
		xmlHttp.onreadystatechange = function() { 
			if (xmlHttp.readyState == XMLHttpRequest.DONE && xmlHttp.status == 200){
				callback(xmlHttp.responseText);
			}
		}

	xmlHttp.open("POST", theUrl, true);

	xmlHttp.setRequestHeader("Content-Type", "application/json");

	xmlHttp.send(data)
}