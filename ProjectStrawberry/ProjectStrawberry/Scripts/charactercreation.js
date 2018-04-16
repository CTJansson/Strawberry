var totalOffensive = 0;
var totalDefensive = 0;
var totalWeaponMastery = 0;

$(document).ready(function () {
    showHideAvatars();
    previewImage();
})

function calculateOffensive() {
    var staminaPoints = $("#Stamina").val();
    var strengthPoints = $("#Strength").val();
    var quicknessPoints = $("#Quickness").val();

    var total = parseInt(staminaPoints) + parseInt(strengthPoints) + parseInt(quicknessPoints);
    totalOffensive = total;

    $("#OffensiveAmount").text(totalOffensive);

    calculateTotal();
}

function calculateDefensive() {
    var blockPoints = $("#Block").val();
    var evasionPoints = $("#Evasion").val();
    var parryPoints = $("#Parry").val();
    var vitalityPoints = $("#Vitality").val();


    var total = parseInt(blockPoints) + parseInt(evasionPoints) + parseInt(parryPoints) + parseInt(vitalityPoints);
    totalDefensive = total;

    $("#DefensiveAmount").text(totalDefensive);

    calculateTotal();
}

function calculateWeaponMastery() {
    var axePoints = $("#Axe").val();
    var daggerPoints = $("#Dagger").val();
    var macePoints = $("#Mace").val();
    var polearmPoints = $("#Polearm").val();
    var spearPoints = $("#Spear").val();
    var swordPoints = $("#Sword").val();

    var total = parseInt(axePoints) + parseInt(daggerPoints) + parseInt(macePoints) + parseInt(polearmPoints) + parseInt(spearPoints) + parseInt(swordPoints);
    totalWeaponMastery = total;

    $("#WeaponMasteryAmount").text(totalWeaponMastery);

    calculateTotal();
}

function calculateTotal() {
    total = parseInt(totalOffensive) + parseInt(totalDefensive) + parseInt(totalWeaponMastery);

    if (total === 60) {
        document.getElementById("TotalAmount").className = "label label-success ";
        $("#TotalAmount").text(total + " / 60");
    } else {
        document.getElementById("TotalAmount").className = "label label-danger";
        $("#TotalAmount").text(total + " / 60");
    }

}

function previewImage() {
    var gender = $("#GenderId").val();

    if (gender == "1") {
        var imageName = $("#AvatarFemale").val();

        $("#previewFemaleAvatar").attr('src', "../Content/Images/AvatarFemales/" + imageName + ".jpg");
    } else {
        var imageName = $("#AvatarMale").val();

        $("#previewMaleAvatar").attr('src', "../Content/Images/AvatarMales/" + imageName + ".jpg");
    }
}

function showHideAvatars() {
    var gender = $("#GenderId").val();

    if (gender == 1) {
        $('#FemaleAvatars').show();
        $('#MaleAvatars').hide();
        previewImage();

    } else {
        $('#FemaleAvatars').hide();
        $('#MaleAvatars').show();
        previewImage();
    }
}