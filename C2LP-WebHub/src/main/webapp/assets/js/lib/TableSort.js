/**
 * Created by hua on 2015/8/5.
 */
$(function () {
    $("table th:nth-child(2)").attr("data-toggle","true");
    $("table tr th:nth-child(2)~th").attr("data-hide","phone");
    $("table tr th:first-child").attr("data-hide","phone");
    var $remarkOrStageString = $("table tr:first-child th:last-child");
    if($remarkOrStageString.html() && $remarkOrStageString.html().trim()=="操作"){
        $remarkOrStageString.attr("data-sort-ignore","true");
    }
    $('table').footable();
});