// TODO make a method to get current logged in user

// TODO make a method to get characters
function getUserInfos(callback)
{
	sendHttpGetRequest("/user", function(response) 
	{
		var user = JSON.parse(response);

		callback(user)
	});
}