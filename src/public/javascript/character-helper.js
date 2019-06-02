// TODO make a method to get characters
function getUserCharacters(callback)
{
	sendHttpGetRequest("/api/characters", function(response) 
	{
		var characters = JSON.parse(response);

		characters.forEach(character => {

			character["data"] = JSON.parse(character["data"]);
		});

		callback(characters)
	});
}

function deleteCharacter(id)
{
	sendHttpPostRequest("/api/character/" + id + "/delete", null , () => {

		location.reload();
	})
}

function viewCharacterSheet(id)
{

	location.href = "/character/" + id + "/sheet";
	// TODO navigate to character sheet
}