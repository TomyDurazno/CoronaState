//READY
$(() => {
    top5bydeaths();
    top5bydeathstoday();
    casesByStatus();
    casesByStatusSouth();
    byContinent();
    lineChart();
})

function top5bydeaths() {

    function onSuccess(data)
    {
        console.log("top5muertes");
        console.log(data);
        s = {
            type: "pie",
            data: data,
            options: {
                responsive: !0,
                legend: {
                    position: "top"
                },
                title: {
                    display: !1,
                    text: "Chart.js Doughnut Chart"
                },
                animation: {
                    animateScale: !0,
                    animateRotate: !0
                }
            }
        };

        var pie = document.getElementById("chart-area").getContext("2d");
        window.myPie2 = new Chart(pie, s);
    }

    $.ajax({
        url: "/top5bydeaths",
        success: onSuccess
    })
}

function top5bydeathstoday() {

    function onSuccess(data) {

        u = {
            type: "pie",
            data: data,
            options: {
                responsive: !0
            }
        }

        var n = document.getElementById("doughnut-chart").getContext("2d");
        window.myDoughnut = new Chart(n, u);
    }

    $.ajax({
        url: "/top5bydeathstoday",
        success: onSuccess
    })
}

function casesByStatus() {

    function onSuccess(data) {

        d = {
            type: "bar",
            data: data,
            options: {
                title: {
                    display: !0
                },
                tooltips: {
                    mode: "index",
                    intersect: !1
                },
                responsive: !0,
                scales: {
                    xAxes: [{
                        stacked: !0
                    }],
                    yAxes: [{
                        stacked: !0
                    }]
                }
            }
        };

        window.myBar = new Chart(document.getElementById("stacked-bars-chart").getContext("2d"), d);
    }

    $.ajax({
        url: "/casesbystatus",
        success: onSuccess
    })
}

function casesByStatusSouth() {

    function onSuccess(data) {

        d = {
            type: "bar",
            data: data,
            options: {
                title: {
                    display: !0
                },
                tooltips: {
                    mode: "index",
                    intersect: !1
                },
                responsive: !0,
                scales: {
                    xAxes: [{
                        stacked: !0
                    }],
                    yAxes: [{
                        stacked: !0
                    }]
                }
            }
        };

        window.myBar = new Chart(document.getElementById("stacked-bars-chart-south").getContext("2d"), d);
    }

    $.ajax({
        url: "/casesbystatus?south=true",
        success: onSuccess
    })
}

function byContinent() {

    function onSuccess(data) {

        console.log("byContinent");

        s = {
            type: "pie",
            data: data,
            options: {
                responsive: !0
            }
        };

        console.log(s.data);

        var pie = document.getElementById("chart-area-continents").getContext("2d");
        window.myPie2 = new Chart(pie, s);
    }

    $.ajax({
        url: "/byContinent",
        success: onSuccess
    });
}

function lineChart()
{
    function onSuccess(data) {
        s = {
            type: "line",
            data: data,
            options: {
                maintainAspectRatio: true,
                spanGaps: false,
                elements: {
                    line: {
                        tension: 0.000001
                    }
                },
                scales: {
                    yAxes: [{
                        stacked: true
                    }]
                },
                plugins: {
                    filler: {
                        propagate: false
                    },
                    'samples-filler-analyser': {
                        target: 'chart-analyser'
                    }
                }
            }
        };

        var pie = document.getElementById("line-chart").getContext("2d");

        window.myLineChart = new Chart(pie, s);        
    }

    $.ajax({
        url: "/linechart",
        success: onSuccess
    })
}