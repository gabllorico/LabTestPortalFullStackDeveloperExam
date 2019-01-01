function search(url, searchBoxId) {
    var searchValue = document.getElementById(searchBoxId).value;
    window.location.href = url + '?search=' + searchValue;
}