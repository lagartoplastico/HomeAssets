function confirmDelete(uniqueId, isDeleteClicked) {
    var delelteSpan = "deleteSpan_" + uniqueId
    var confirmDeleteSpan = "confirmDeleteSpan_" + uniqueId
    var editLink = "editLink_" + uniqueId
    var backLink = "backLink_" + uniqueId

    if (isDeleteClicked) {
        $('#' + delelteSpan).hide()
        $('#' + editLink).hide()
        $('#' + backLink).hide()

        $('#' + confirmDeleteSpan).show()
    } else {
        $('#' + delelteSpan).show()
        $('#' + editLink).show()
        $('#' + backLink).show()

        $('#' + confirmDeleteSpan).hide()
    }
}