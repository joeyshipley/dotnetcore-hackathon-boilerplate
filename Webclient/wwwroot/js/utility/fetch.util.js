BOS.UTIL.FETCH = {

  post: function(url, data) {
    return $.ajax({
        type: "POST",
        url: url,
        data: JSON.stringify(data),
        contentType: "application/json; charset=utf-8",
        dataType: "json"
    });
  }

};