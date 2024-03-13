// Please see documentation at https://docs.microsoft.com/aspnet/core/client-side/bundling-and-minification
// for details on configuring this project to bundle and minify static web assets.

// Write your JavaScript code.

let apiURL = "https://forkify-api.herokuapp.com/api/v2/recipes"
let apikey = "4ab73696-d61f-465b-9336-7f9ad55b259e"

async function GetRecipes(recipeName,id,isAllShow) {
    let resp = await fetch(`${apikey}?search=${recipeName}&key=${apikey}`);
    let result = await resp.json();
    console.log(result);
    let Recipes = isAllShow ? result.data.recipes : result.data.recipes.slice(0, 5)
    showRecipes(showRecipes, id);
}
function showRecipes(recipes, id) {
    $.ajax({
        contentType: "application/json; charset=utf-8",
        html: 'html',
        type: 'POST',
        url: '/Recipe/GetRecipeCard',
        data: JSON.stringify(recipes),
        success: function (htmlResult) {
            $('#' + id).html(htmlResult);
        }
        })
}