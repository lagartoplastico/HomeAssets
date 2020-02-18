var lastUniqueId = null

function confirmDelete(uniqueId, isDeleteClicked) {

    if (lastUniqueId != null) {
        $('#' + "deleteSpan_" + lastUniqueId).show()
        $('#' + "editLink_" + lastUniqueId).show()
        $('#' + "backLink_" + lastUniqueId).show()

        $('#' + "confirmDeleteSpan_" + lastUniqueId).hide()
    }

    var deleteSpan = "deleteSpan_" + uniqueId;
    var confirmDeleteSpan = "confirmDeleteSpan_" + uniqueId
    var editLink = "editLink_" + uniqueId
    var backLink = "backLink_" + uniqueId

    if (isDeleteClicked) {
        $('#' + deleteSpan).hide()
        $('#' + editLink).hide()
        $('#' + backLink).hide()

        $('#' + confirmDeleteSpan).show()
    } else {
        $('#' + deleteSpan).show()
        $('#' + editLink).show()
        $('#' + backLink).show()

        $('#' + confirmDeleteSpan).hide()
    }

    lastUniqueId = uniqueId
}