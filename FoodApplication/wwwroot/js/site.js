// Please see documentation at https://docs.microsoft.com/aspnet/core/client-side/bundling-and-minification
// for details on configuring this project to bundle and minify static web assets.

// Write your JavaScript code.

const apiURL = "https://forkify-api.herokuapp.com/api/v2/recipes";
const apiKey = "b4a57a58-feb4-4e70-8015-f45242e63c38"


async function GetRecipes(recipeName, id, isAllShow) {
    let resp = await fetch(`${apiURL}?search=${recipeName}&key=${apiKey}`);
    let result = await resp.json();
    //console.log(result);
    let recipes = isAllShow ? result.data.recipes : result.data.recipes.slice(1, 7);
    showRecipes(recipes, id);
}

function showRecipes(recipes, id) {
    $.ajax({
        contentType: 'application/json; charset=utf-8',
        datatype: "html",
        type: 'POST',
        url: '/Recipe/GetRecipeCard',
        data: JSON.stringify(recipes),
        success: function (htmlResult) {
            console.log(id);
            $('#' + id).html(htmlResult);
        }
    })
}

async function getOrderRecipe(id) {
    let resp = await fetch(`${apiURL}/${id}?key=${apiKey}`);
    let result = await resp.json();
    console.log(result);
}