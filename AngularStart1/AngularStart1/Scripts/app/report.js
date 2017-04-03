angular.module('main')
    .controller('reportController', ['$scope', 'appService', function ($scope, appService) {
        $scope.ShowannualReport = false;
        $scope.showMonthlyReport = false;




        $scope.annualReport = function () {
           
            appService.getannualReport().then(function (d) {

                $scope.annualReportParm = d.data;
              
                $scope.ShowannualReport = true;
                $scope.showMonthlyReport = false;
                $scope.drowAnuallReport();

            }, function (error) {
                alert("Something went wrong , try again !!! ");
            });



            $scope.drowAnuallReport = function () {
                $scope.chartObject = {};
                $scope.chartObject.type = "ColumnChart";
                $scope.lastLastYear = [
                    { v: $scope.annualReportParm.YearC },
                            { v: $scope.annualReportParm.CountC, f: $scope.annualReportParm.CountC + " " },
                            { v: $scope.annualReportParm.UsegC, f: $scope.annualReportParm.UsegC + " " }
                ];
                $scope.lastYear = [
                 { v: $scope.annualReportParm.YearB },
                         { v: $scope.annualReportParm.CountB, f: $scope.annualReportParm.CountB + " " },
                         { v: $scope.annualReportParm.UsegB, f: $scope.annualReportParm.UsegB + " " }
                ];
                $scope.now = [
                 { v: $scope.annualReportParm.YearA },
                         { v: $scope.annualReportParm.CountA, f: $scope.annualReportParm.CountA + " " },
                         { v: $scope.annualReportParm.UsegA, f: $scope.annualReportParm.UsegA + " " }
                ];
                $scope.chartObject.data = {
                    "cols": [
                        { id: "t", label: "Topping", type: "string" },
                        { id: "g", label: "Gift card purchases", type: "number" },
                        { id: "i", label: "Credit card implemented", type: "number" }
                    ], "rows": [
                       { c: $scope.lastLastYear },

                       { c: $scope.lastYear },

                       { c: $scope.now }
                    ]
                };
                $scope.chartObject.options = {
                    'title': 'annual report',
                    ' legend': 'none',
                    'bar': { groupWidth: '95%' },
                    'vAxis': { gridlines: { count: 4 } }
                };
            };
        }



        $scope.monthlyReport = function () {

            // for ng-rep per year
            $scope.yearABuy = [];
            $scope.yearAUse = [];

            $scope.yearBBuy = [];
            $scope.yearBUse = [];

            $scope.yearCBuy = [];
            $scope.yearCUse = [];

            $scope.ShowannualReport = false;
            $scope.showMonthlyReport = true;

            appService.getmonthlyReport().then(function (d) {

                $scope.monthlyReportParm = d.data;


                $scope.drowmonthlyReport();

            }, function (error) {
                alert("Something went wrong , try again !!! ");
            });



            $scope.drowmonthlyReport = function () {



                $scope.Nowyear = {};
                $scope.Nowyear.type = "ColumnChart";

                $scope.Lastyear = {};
                $scope.Lastyear.type = "ColumnChart";

                $scope.LastLastyear = {};
                $scope.LastLastyear.type = "ColumnChart";
                //for (var year = 0; year < 2; year++) {

                for (var month = 0; month < 36; month++) {


                    switch (month) {
                        case 24:
                        case 12:
                        case 0:
                            $scope.January = [
                                      { v: "Jan" },
                                       { v: $scope.monthlyReportParm.CountOfBuying[month], f: $scope.monthlyReportParm.CountOfBuying[month] + "" },
                                        { v: $scope.monthlyReportParm.CountOfImplement[month], f: $scope.monthlyReportParm.CountOfImplement[month] + "" }
                            ];
                            break;
                        case 25:
                        case 13:
                        case 1:
                            $scope.February = [
                              { v: "Feb" },
                               { v: $scope.monthlyReportParm.CountOfBuying[month], f: $scope.monthlyReportParm.CountOfBuying[month] + "" },
                               { v: $scope.monthlyReportParm.CountOfImplement[month], f: $scope.monthlyReportParm.CountOfImplement[month] + "" }
                            ];
                            break;
                        case 26:
                        case 14:
                        case 2:
                            $scope.March = [
                             { v: "March" },
                              { v: $scope.monthlyReportParm.CountOfBuying[month], f: $scope.monthlyReportParm.CountOfBuying[month] + "" },
                              { v: $scope.monthlyReportParm.CountOfImplement[month], f: $scope.monthlyReportParm.CountOfImplement[month] + "" }
                            ];
                            break;
                        case 27:
                        case 15:
                        case 3:
                            $scope.April = [
                             { v: "April" },
                              { v: $scope.monthlyReportParm.CountOfBuying[month], f: $scope.monthlyReportParm.CountOfBuying[month] + "" },
                              { v: $scope.monthlyReportParm.CountOfImplement[month], f: $scope.monthlyReportParm.CountOfImplement[month] + "" }
                            ];
                            break;
                        case 28:
                        case 16:
                        case 4:
                            $scope.May = [
                              { v: "May" },
                               { v: $scope.monthlyReportParm[month], f: $scope.monthlyReportParm[month] + "" },
                               { v: $scope.monthlyReportParm.CountOfImplement[month], f: $scope.monthlyReportParm.CountOfImplement[month] + "" }
                            ];
                            break;
                        case 29:
                        case 17:
                        case 5:
                            $scope.June = [
                             { v: "June" },
                              { v: $scope.monthlyReportParm.CountOfBuying[month], f: $scope.monthlyReportParm.CountOfBuying[month] + "" },
                              { v: $scope.monthlyReportParm.CountOfImplement[month], f: $scope.monthlyReportParm.CountOfImplement[month] + "" }
                            ];
                            break;
                        case 30:
                        case 18:
                        case 6:
                            $scope.July = [
                             { v: "July" },
                              { v: $scope.monthlyReportParm.CountOfBuying[month], f: $scope.monthlyReportParm.CountOfBuying[month] + "" },
                              { v: $scope.monthlyReportParm.CountOfImplement[month], f: $scope.monthlyReportParm.CountOfImplement[month] + "" }
                            ];
                            break;
                        case 31:
                        case 19:
                        case 7:
                            $scope.August = [
                             { v: "Aug" },
                              { v: $scope.monthlyReportParm.CountOfBuying[month], f: $scope.monthlyReportParm.CountOfBuying[month] + "" },
                              { v: $scope.monthlyReportParm.CountOfImplement[month], f: $scope.monthlyReportParm.CountOfImplement[month] + "" }
                            ];
                            break;
                        case 32:
                        case 20:
                        case 8:
                            $scope.September = [
                             { v: "Sept" },
                              { v: $scope.monthlyReportParm.CountOfBuying[month], f: $scope.monthlyReportParm.CountOfBuying[month] + "" },
                              { v: $scope.monthlyReportParm.CountOfImplement[month], f: $scope.monthlyReportParm.CountOfImplement[month] + "" }
                            ];
                            break;
                        case 33:
                        case 21:
                        case 9:
                            $scope.October = [
                             { v: "Oct" },
                              { v: $scope.monthlyReportParm.CountOfBuying[month], f: $scope.monthlyReportParm.CountOfBuying[month] + "" },
                              { v: $scope.monthlyReportParm.CountOfImplement[month], f: $scope.monthlyReportParm.CountOfImplement[month] + "" }
                            ];
                            break;
                        case 34:
                        case 22:
                        case 10:
                            $scope.November = [
                             { v: "Nov" },
                              { v: $scope.monthlyReportParm.CountOfBuying[month], f: $scope.monthlyReportParm.CountOfBuying[month] + "" },
                              { v: $scope.monthlyReportParm.CountOfImplement[month], f: $scope.monthlyReportParm.CountOfImplement[month] + "" }
                            ];
                            break;
                        case 35:
                        case 23:
                        case 11:
                            $scope.December = [
                             { v: "Dec" },
                              { v: $scope.monthlyReportParm.CountOfBuying[month], f: $scope.monthlyReportParm.CountOfBuying[month] + "" },
                              { v: $scope.monthlyReportParm.CountOfImplement[month], f: $scope.monthlyReportParm.CountOfImplement[month] + "" }
                            ];
                            break;
                    }





                    if (month == 11) {


                        $scope.Nowyear.data = {
                            "cols": [
                                { id: "t", label: "Topping", type: "string" },
                                { id: "g", label: "Gift card purchases", type: "number" },
                                { id: "i", label: "Credit card implemented", type: "number" }
                            ], "rows": [
                               { c: $scope.January },
                               { c: $scope.February },
                               { c: $scope.March },
                               { c: $scope.April },
                               { c: $scope.May },
                               { c: $scope.June },
                               { c: $scope.July },
                               { c: $scope.August },
                               { c: $scope.September },
                               { c: $scope.October },
                               { c: $scope.November },
                               { c: $scope.December }
                            ]
                        };
                        $scope.Nowyear.options = {
                            'title': 'monthly report 2015 year',
                            ' legend': 'none',
                            'bar': { groupWidth: '60%' },
                            'vAxis': { gridlines: { count: 4 } }
                        };
                    }
                    if (month == 23) {
                        $scope.Lastyear.data = {
                            "cols": [
                                { id: "t", label: "Topping", type: "string" },
                                { id: "g", label: "Gift card purchases", type: "number" },
                                { id: "i", label: "Credit card implemented", type: "number" }
                            ], "rows": [
                               { c: $scope.January },
                               { c: $scope.February },
                               { c: $scope.March },
                               { c: $scope.April },
                               { c: $scope.May },
                               { c: $scope.June },
                               { c: $scope.July },
                               { c: $scope.August },
                               { c: $scope.September },
                               { c: $scope.October },
                               { c: $scope.November },
                               { c: $scope.December }
                            ]
                        };
                        $scope.Lastyear.options = {
                            'title': 'monthly report 2014 year',
                            ' legend': 'none',
                            'bar': { groupWidth: '60%' },
                            'vAxis': { gridlines: { count: 4 } }
                        };
                    }
                    if (month == 35) {
                        $scope.LastLastyear.data = {
                            "cols": [
                                { id: "t", label: "Topping", type: "string" },
                                { id: "g", label: "Gift card purchases", type: "number" },
                                { id: "i", label: "Credit card implemented", type: "number" }
                            ], "rows": [
                               { c: $scope.January },
                               { c: $scope.February },
                               { c: $scope.March },
                               { c: $scope.April },
                               { c: $scope.May },
                               { c: $scope.June },
                               { c: $scope.July },
                               { c: $scope.August },
                               { c: $scope.September },
                               { c: $scope.October },
                               { c: $scope.November },
                               { c: $scope.December }
                            ]
                        };
                        $scope.LastLastyear.options = {
                            'title': 'monthly report 2013 year',
                            ' legend': 'none',
                            'bar': { groupWidth: '60%' },
                            'vAxis': { gridlines: { count: 4 } }
                        };
                    }
                }
                for (var i = 0; i < 36; i++) {
                    if (i < 12) {
                        $scope.yearABuy.push($scope.monthlyReportParm.CountOfBuying[i]);
                        $scope.yearAUse.push($scope.monthlyReportParm.CountOfImplement[i]);
                    }
                    if (i > 11 && i < 24) {
                        $scope.yearBBuy.push($scope.monthlyReportParm.CountOfBuying[i]);
                        $scope.yearBUse.push($scope.monthlyReportParm.CountOfImplement[i]);
                    }
                    if (i > 23) {
                        $scope.yearCBuy.push($scope.monthlyReportParm.CountOfBuying[i]);
                        $scope.yearCUse.push($scope.monthlyReportParm.CountOfImplement[i]);
                    }
                  
                }
            }

        };
        $scope.exportDataAnnual = function () {
            var blob = new Blob([document.getElementById('exportableAnnual').innerHTML], {
                type: "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet;charset=utf-8"
            });
            saveAs(blob, "Report.xls");
        }; 

        $scope.exportDataMonthly = function () {
            var blob = new Blob([document.getElementById('exportableMonthly').innerHTML], {
                type: "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet;charset=utf-8"
            });
            saveAs(blob, "Report.xls");
        };
        /*print report*/
        $scope.print = function () {
                     
            if (0 === ($('.owl-page.active').index())) {               
                $('#printAnnualA').printThis();
            }
          else  if (1 === $('.owl-page.active').index()) {               
                $('#printAnnualB').printThis();
            }
          else if (2 === $('.owl-page.active').index()) {
                $('#printAnnualC').printThis();
          }
         
             

          
           
        }
        $scope.printAnnual = function () {
            $('#printAnnual').printThis();
        }
    }]);
 


