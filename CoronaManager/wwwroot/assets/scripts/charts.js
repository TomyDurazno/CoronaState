$(() => {
    pieChart();
    donaChart();
    stackedChart();
    stackedChartsouth();
})

function pieChart() {

    function onSuccess(data)
    {
        s = {
            type: "pie",
            data: {
                datasets: [{
                    data: data.map(d => d.amount),
                    backgroundColor: [window.chartColors.red, window.chartColors.orange, window.chartColors.yellow, window.chartColors.green, window.chartColors.blue],
                    label: "Top 5 Muertes"
                }],
                labels: data.map(d => d.country)
            },
            options: {
                responsive: !0
            }
        };

        var pie = document.getElementById("chart-area").getContext("2d");
        window.myPie2 = new Chart(pie, s);
    }

    $.ajax({
        url: "/top5muertes",
        success: onSuccess
    })
}

function donaChart() {

    function onSuccess(data) {

        u = {
            type: "doughnut",
            data: {
                datasets: [{
                    data: data.map(d => d.amount),
                    backgroundColor: [window.chartColors.red, window.chartColors.orange, window.chartColors.yellow, window.chartColors.green, window.chartColors.blue],
                    label: "Top 5 Muertes Hoy"
                }],
                labels: data.map(d => d.country)
            },
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
        }

        var n = document.getElementById("doughnut-chart").getContext("2d");
        window.myDoughnut = new Chart(n, u);
    }

    $.ajax({
        url: "/top5muertesdehoy",
        success: onSuccess
    })
}

function stackedChart() {

    function onSuccess(data) {

        data.datasets.forEach((d, i) => d.backgroundColor = globalColors[i]);

        var barChartData = {
            labels: data.labels,
            datasets: data.datasets
        };

        d = {
            type: "bar",
            data: barChartData,
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
        url: "/stackedbars",
        success: onSuccess
    })
}

function stackedChartsouth() {

    function onSuccess(data) {

        data.datasets.forEach((d, i) => d.backgroundColor = globalColors[i]);

        var barChartData = {
            labels: data.labels,
            datasets: data.datasets
        };

        d = {
            type: "bar",
            data: barChartData,
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

globalColors = [ window.chartColors.red, window.chartColors.blue, window.chartColors.green, window.chartColors.yellow, window.chartColors.orange, window.chartColors.purple, window.chartColors.grey];