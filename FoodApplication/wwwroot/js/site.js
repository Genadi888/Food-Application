// Please see documentation at https://docs.microsoft.com/aspnet/core/client-side/bundling-and-minification
// for details on configuring this project to bundle and minify static web assets.

// Write your JavaScript code.

const apiURL = "https://forkify-api.herokuapp.com/api/v2/recipes";
const apiKey = "b4a57a58-feb4-4e70-8015-f45242e63c38"


async function GetRecipes(recipeName, id, isAllShow) {
    const resp = await fetch(`${apiURL}?search=${recipeName}&key=${apiKey}`);
    const result = await resp.json();
    //console.log(result);
    const recipes = isAllShow ? result.data.recipes : result.data.recipes.slice(1, 7);
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
            getAddedCart();
        }
    });
}

async function getOrderRecipe(id, showId) {
    let resp = await fetch(`${apiURL}/${id}?key=${apiKey}`);
    let result = await resp.json();
    let recipe = result.data.recipe;
    showOrderRecipeDetails(recipe, showId);
}

function showOrderRecipeDetails(orderRecipeDetails, showId) {
    $.ajax({
        datatype: "html",
        type: 'POST',
        url: '/Recipe/ShowOrder',
        data: (orderRecipeDetails),
        success: function (htmlResult) {
            $('#' + showId).html(htmlResult);
        }
    });
}
// order page
function quantity(option) {
    let qty = $('#qty').val();
    let price = parseInt($('#price').val());
    let totalAmount = 0;
    if (option === 'inc') {
        qty = parseInt(qty) + 1;
    }
    else {
        qty = qty == 1 ? qty : qty - 1;
    }
        price = price * qty;
    $('#qty').val(qty);
    $('#totalAmount').val(price);
}

// Add to cart

async function cart() {
    let iTag = $(this).children('i')[0];
    let recipeId = $(this).attr('data-recipeId');
    console.log(recipeId);
    if ($(iTag).hasClass('fa-regular')) {
        let resp = await fetch(`${apiURL}/${recipeId}?key=${apiKey}`);
        let result = await resp.json();
        let cart = result.data.recipe;
        cart.RecipeId = recipeId;
        delete cart.id;
        cartRequest(cart, 'SaveCart','fa-solid', 'fa-regular', iTag);

    } else {
        let data = { Id:recipeId};
        cartRequest(data, 'RemoveCartFromList', 'fa-regular', 'fa-solid', iTag);
    }
}

function cartRequest(data, action, addcls, removecls,iTag) {
    $.ajax({
        url: '/Cart/'+ action,
        type: 'Post',
        date: data,
        success: function (resp) {
            $(iTag).addClass(addcls);
            $(iTag).removeClass(removecls);
        },
        error: function (err) {
            console.log(err);
        }
    });
}
function getAddedCart() {
    $.ajax({
        url: '/Cart/GetAddedCarts',
        type: 'GET',
        dataType: 'json',
        success: function (result) {
            $('.addToCartIcon').each((index, spanTag) => {
                let recipeId = $(spanTag).attr("data-recipeId");
                for (var i = 0; i < result.length; i++) {
                    if (resipeId==result[i]) {
                        $(this).children('i')[0];
                        let itag = $(spanTag).children('i')[0];
                        $(itag).addClass('fa-solid');
                        $(itag).removeClass('fa-regular');
                        break;
                    }
                }
            })

        },
        error: function (err) {
            console.log(err);
        }

    });
}

function getCartList() {
    $.ajax({
        url: '/Cart/GetCartList',
        type:'GET',
        dataType:'html',
        success: function (result) {
            $('#showCartList').html(result);
        },  
        error: function (err) {
            console.log(err);
        }
    });
}


