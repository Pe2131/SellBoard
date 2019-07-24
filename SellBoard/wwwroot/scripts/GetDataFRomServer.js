var SumOfeffectiveCalls = 0;
var SumofCards = 0;
var SumOfFtd = 0;
var start = 0;
var end = 10;
var count = 1;
var lastcheckC = { "StageName": "", "DepositDate": "", "AmountEur": "" };
var lastcheckR = { "StageName": "", "DepositDate": "", "AmountEur": "" };

function GetDataFromServer() {

    $.ajax({
        type: "Post",
        jsonp: "callback",
        dataType: "jsonp",
        headers: {
            "x-trackbox-username": 'Basic xxxxxxxxxxxxx',
            "x-trackbox-password": 'xxxxxxxxxxxxxxxxxxxx',
            "x-api-key": '',
            "Content-Type": "application/json"
        },
        url: "https://platform.domain.com/api/pull/customers",
        data: { 'from': GetTodayDate(), 'to': GetTodayDate(), 'type': '3' },
        success: function(response) {
            console.log(response);
            var s = JSON.stringify(response);
            var jsonData = JSON.parse(s);
            SumDailyConversionServer(jsonData);
            if (Object.keys(jsonData.data).length > 0) {
                console.log(jsonData);
            } else {
                console.log("Get data but has Error ");
            }
        },
        error: function() {
            console.log("Error");
        }
    });
}

function InitialLastSale() {
    // initial Conversion last sale
    $.getJSON("http://10.30.30.220:9001/sales/websalestv/getNewDeposits?branchid=1&DepartmentGroupId=2", function(res) {

        if (res != null) {
            var temp = JSON.stringify(res);
            var jsonData = JSON.parse(temp);
            var result = jsonData.Message[0];
            lastcheckC = result;
        }
    });
    // initial retention last sale
    $.getJSON("http://10.30.30.220:9001/sales/websalestv/getNewDeposits?branchid=1&DepartmentGroupId=3", function(res) {

        if (res != null) {
            var temp = JSON.stringify(res);
            var jsonData = JSON.parse(temp);
            var result = jsonData.Message[0];
            lastcheckR = result;
        }
    });
    window.setTimeout(function() {
        if (compareLastdateTime(lastcheckC.DepositDate, lastcheckR.DepositDate)) {
            DifrenceTime(lastcheckC.DepositDate);
        } else {
            DifrenceTime(lastcheckR.DepositDate)
        }
        console.log('Initial Conversion:');
        console.log(lastcheckC);
        console.log('Initial Retention');
        console.log(lastcheckR);
    }, 2000);
}

function CheckSales() {
    var resettime = false;
    $.getJSON("http://10.30.30.220:9001/sales/websalestv/getNewDeposits?branchid=1&DepartmentGroupId=2", function(res) {

        if (res != null) {
            var temp = JSON.stringify(res);
            var jsonData = JSON.parse(temp);
            var result = jsonData.Message[0];
            if (!_.isEqual(lastcheckC, result)) {
                //alert('horraa');
                resettime = true;
                lastcheckC = result;
                window.setTimeout(function() {
                    if (resettime) {
                        logSale('Conversion', lastcheckC);
                        ShowSale(lastcheckC);
                    }
                }, 1000);
                // $('element_to_pop_up').append(lastcheckC.StageName);
            }
        }
    });
    $.getJSON("http://10.30.30.220:9001/sales/websalestv/getNewDeposits?branchid=1&DepartmentGroupId=3", function(res) {

        if (res != null) {
            var temp = JSON.stringify(res);
            var jsonData = JSON.parse(temp);
            var result = jsonData.Message[0];
            if (!_.isEqual(lastcheckR, result)) {
                // alert('horraa');
                lastcheckR = result;
                resettime = true;
                window.setTimeout(function() {
                    if (resettime) {
                        logSale('Retention', lastcheckR);
                        ShowSale(lastcheckR);
                    }
                }, 1000);
                //  $('element_to_pop_up').append(lastcheckC.StageName);
            }
        }
    });
    window.setTimeout(function() { if (resettime) { ResetTime(); } }, 1000);
}

function ShowSale(sale) {
    $('#LastSaleName').empty();
    $('#Price').empty();
    $('#LastSaleName').append(sale.StageName);
    $('#Price').append('' + currency(sale.AmountEur));
    window.setInterval(function() { $("#myModal").modal('hide'); }, 3000); //modal hide
}

function logSale(grouptype, info) {
    console.log(grouptype);
    console.log(info);
    console.log('----------------');
}

function restStartEnd() {
    start = 0;
    end = 10;
}