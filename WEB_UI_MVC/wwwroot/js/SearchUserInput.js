var searchInput = document.getElementById('searchInput');
searchInput.addEventListener('input', function () {
    var searchText = searchInput.value.trim().toLowerCase();
    var rows = document.querySelectorAll('tbody tr');
    rows.forEach(function (row) {
        var username = row.querySelector('td:first-child').textContent.trim().toLowerCase();
        if (username.includes(searchText)) {
            row.style.display = 'table-row';
        } else {
            row.style.display = 'none';
        }
    });
});