function buyShield(id) {
    var shieldId = id;

    $.ajax({
        url: '/Market/BuyShield/' + shieldId,
        type: 'GET',
        success: function (result) {
            opendiv(result);
        }
    })
}

function buyWeapon(id) {
    var weaponId = id;

    $.ajax({
        url: '/Market/BuyWeapon/' + weaponId,
        type: 'GET',
        success: function (result) {
            opendiv(result);
        }
    })
}

function buyArmor(id) {
    var armorId = id;
    $.ajax({
        url: '/Market/BuyArmor/' + armorId,
        type: 'GET',
        success: function (result) {
            opendiv(result);
        }
    })
}