$(function () {
    $("#navbar-placeholder").load("navbar.html");
});
$(document).ready(function () {
    // Open form to create new receipt
    $('#createReceiptBtn').on('click', function () {
        console.log('Clicked on create receipt button');
        openForm('receipt');
    });

    // Open form to create new return
    $('#createReturnBtn').on('click', function () {
        console.log('Clicked on create return button');
        openInvoiceCheckForm();
    });

    // Handle invoice check
    $('#checkInvoiceBtn').on('click', function () {
        console.log('Clicked on check invoice button');
        checkInvoice();
    });

    // Handle form submission
    $('#recordForm').on('submit', function (event) {
        event.preventDefault();
    });

    // Calculate total price
    $('#quantity, #unitPrice').on('input', function () {
        calculateTotalPrice();
    });

    // Function to open form
    function openForm(type, invoiceCode = '') {
        resetForm();
        const now = new Date().toISOString().split('T')[0];
        $('#recordDate').val(now);
        $('#recordType').val(type === 'receipt' ? 'nhap_kho' : 'doi_tra');
        $('#recordForm').show();
        $('#printBtn').show();
        $('#invoiceCheckForm').hide();
        $('#recordCode').val(generateRecordCode());
        $('#employeeId').val(getLoggedEmployeeId());

        // Set the invoice code if it's a return
        if (type === 'return') {
            $('#invoiceCode').val(invoiceCode);
        } else {
            // Hide the invoice code field for receipts
            $('#invoiceCode').closest('.form-group').hide();
        }
    }

    // Function to open invoice check form
    function openInvoiceCheckForm() {
        resetForm();
        $('#invoiceCheckForm').show();
        $('#recordForm').hide();
        $('#printBtn').hide();
    }

    // Function to check invoice
    function checkInvoice() {
        const invoiceCode = $('#invoiceCodeCheck').val().trim();
        if (invoiceCode) {
            if (/^[A-Z0-9]+$/.test(invoiceCode)) {
                $('#invoiceCheckForm').hide();
                openForm('return', invoiceCode);  // Pass the invoice code to openForm
            } else {
                $('#invoiceCheckError').text('Mã hóa đơn chỉ được chứa chữ hoa và số');
            }
        } else {
            $('#invoiceCheckError').text('Vui lòng nhập mã hóa đơn');
        }
    }

    // Function to reset form
    function resetForm() {
        var recordForm = document.getElementById('recordForm');
        if (recordForm) {
            recordForm.reset();
        }

        var invoiceCodeCheck = document.getElementById('invoiceCodeCheck');
        if (invoiceCodeCheck) {
            invoiceCodeCheck.value = '';
        }

        var invoiceCode = document.getElementById('invoiceCode');
        if (invoiceCode) {
            invoiceCode.value = '';
        }

        var errorMessages = document.getElementsByClassName('error-message');
        for (var i = 0; i < errorMessages.length; i++) {
            errorMessages[i].textContent = '';
        }

        // Show all form groups (in case we hid the invoice code for receipts)
        $('.form-group').show();
    }

    // Function to validate form


    // Function to calculate total price
    function calculateTotalPrice() {
        const quantity = parseFloat($('#quantity').val()) || 0;
        const unitPrice = parseFloat($('#unitPrice').val()) || 0;
        const totalPrice = quantity * unitPrice;
        $('#totalPrice').val(totalPrice);
    }

    // Function to generate record code (simplified)
    function generateRecordCode() {
        return 'PN' + Math.random().toString(36).substr(2, 8).toUpperCase();
    }

    // Function to get logged employee ID (simplified)
    function getLoggedEmployeeId() {
        return 'EMP001';
    }
});


function validateForm() {
    let isValid = true;
    let errorMessage = '';

    const recordCode = $('#recordCode').val().trim();
    if (!recordCode) {
        errorMessage += 'Vui lòng nhập mã phiếu\n';
        isValid = false;
    }

    const employeeId = $('#employeeId').val().trim();
    if (!employeeId) {
        errorMessage += 'Vui lòng nhập mã nhân viên\n';
        isValid = false;
    }

    const recordType = $('#recordType').val().trim();
    if (!recordType) {
        errorMessage += 'Vui lòng chọn loại phiếu\n';
        isValid = false;
    }

    const bookCode = $('#bookCode').val().trim();
    if (!bookCode) {
        errorMessage += 'Vui lòng nhập mã sách\n';
        isValid = false;
    } else if (!/^[A-Z0-9]{1,10}$/.test(bookCode)) {
        errorMessage += 'Mã sách không đúng định dạng\n';
        isValid = false;
    }

    const quantity = $('#quantity').val();
    if (quantity <= 0) {
        errorMessage += 'Số lượng phải là số dương\n';
        isValid = false;
    }

    const unitPrice = $('#unitPrice').val();
    if (unitPrice < 0) {
        errorMessage += 'Đơn giá phải là số dương\n';
        isValid = false;
    }

    if (!isValid) {
        alert(errorMessage);
    }

    return isValid;
}
$('#printBtn').on('click', function (e) {
    if (validateForm()) {
        e.preventDefault(); // Ngăn chặn form submit mặc định
        $('#navbar-placeholder').hide();
        $('#tittle').hide();
        $('#tittle2').show();
        $('#createReceiptBtn').hide();
        $('#createReturnBtn').hide();
        $('#printBtn').hide();
        window.print(); // In form
        $('#recordForm').submit(); // Submit form sau khi in
    }

});

// Submit form khi nhấn nút submit
$('#recordForm').on('submit', function (e) {
    // Xử lý form submit tại đây
    e.preventDefault();

    // Bạn có thể thêm mã gửi form bằng ajax hoặc xử lý khác ở đây
    var formData = {
        MaPN: $('#recordCode').val(),
        sMaNV: $('#employeeId').val(),
        dNgaylap: $('#recordDate').val(),
        bLoai: $('#recordType').val() === 'nhap_kho' ? true : false,
        // Các trường khác nếu cần
    };
    console.log('Sending form data:', formData);
    $.ajax({
        type: "POST",
        url: '/cPhieunhap/Create',
        data: formData,
        success: function () {
            alert('Phiếu gửi thành công');
            location.reload(); // Tải lại trang sau khi submit
        },
        error: function () {
            alert('Phiếu gửi bị lỗi');
        }
    });
});

