var ColdChainData = function() {
   // this.init();
}

ColdChainData.prototype.init = function() {
    $("#downpdf").bind("click", this.getPdf);
}

ColdChainData.prototype.getPdf = function() {
    window.open(  URLHelper.downallpdf(  $('#number').val()  )   );
}
