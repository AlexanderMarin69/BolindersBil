$(document).ready(function () {

    $('#checkAll').click(function () {
        $('input:checkbox').prop('checked', this.checked);
    });

    $("#deleteBulk").on('click', function (e) {
        //e.preventDefault();
        getValueUsingParentTag();
    });

    function getValueUsingParentTag() {
        var chkArray = [];

        /* look for all checkboxes in .cards and check if they are checked, and then take their values and push into an array */
        $("input.flo:checked").each(function () {
            chkArray.push($(this).val());
        });

        /* join the array separated by the comma */
        var selected;
        selected = chkArray.join(',');

        /* add selected value to hidden fields if it contains values */
        if (selected.length > 0) {
            $("#vehiclesIdToDelete").val(selected);
            //console.log(selected);
            var value = $("#vehiclesIdToDelete").val();
            //console.log(value);
            //$("#bulkDeleteForm").submit();
        } else {
            alert("Select at least one vehicle");
        }
    }
});

//$(document).ready(function () {
//    var checked = [];
//    $('.checkbox').change(function (e) {
//        if (this.checked) {
//            checked.push($(this).val());
//        } else {
//            var value = $(this).val();
//            var index = checked.indexOf(value);
//            if (index > -1) {
//                checked.splice(index, 1);
//            }
//        }
//        console.log(checked);
//    });

//    $('#checkall').change(function (e) {
//        checked = [];
//        if (this.checked) {
//            $('input.checkbox').prop('checked', true);
//            $('input.checkbox').each(function () {
//                checked.push($(this).val());
//            });
//        } else {
//            $('input.checkbox').prop('checked', false);
//        }
//        console.log(checked);

//        $('input[name="selectedIds"]').val(checked.join(','));
//    });
//});