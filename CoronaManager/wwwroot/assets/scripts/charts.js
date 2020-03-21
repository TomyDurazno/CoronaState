//READY
$(() => {

    var chartsOptions =
     [{
        name: "top5Deaths",
        type: "pie",
        id: "top5Deaths-chart",
        url: "/top5bydeaths"      
    },
    {
        name: "top5DeathsToday",
        type: "pie",
        id: "top5DeathsToday-chart",
        url: "/top5bydeathstoday"
    },
    {
        name: "casesbyContinent",
        type: "pie",
        id: "chart-area-continents",
        url: "/casesbyContinent"
        },
            {
                name: "deathsbyContinent",
                type: "pie",
                id: "chart-area-continents-deaths",
                url: "/deathsbyContinent"
            }];

    chartsOptions.forEach(makeChart);

    var barOptions =
        [
            {
                name: "casesByStatus",
                type: "bar",
                id: "stacked-bars-chart",
                url: "/casesbystatus"
            },
            {
                name: "casesByStatusSouth",
                type: "bar",
                id: "stacked-bars-chart-south",
                url: "/casesbystatussouth"
            }
        ];

    barOptions.forEach(makeBar);

    var lineOptions =
        [{
            name: "linechart",
            type: "line",
            id: "line-chart",
            url: "/linechart"
        },
        {
            name: "linechartalltime",
            type: "line",
            id: "line-chart-all-time",
            url: "/linechartalltime"
            },
            {
                name: "linechartalltimesouth",
                type: "line",
                id: "line-chart-all-time-south",
                url: "/linechartalltimesouth"
            },
            {
                name: "linechartsouth",
                type: "line",
                id: "line-chart-south",
                url: "/linechartsouth"
            }];

    lineOptions.forEach(makeLineChart);
})

function makeChart(options) {

    function onSuccess(data) {

        s = {
            type: options.type,
            data: data,
            options: {
                responsive: !0
            }
        };

        window[options.name] = new Chart(document.getElementById(options.id).getContext("2d"), s);
    }

    $.ajax({
        url: options.url,
        success: onSuccess
    });
}

function makeBar(options) {

    function onSuccess(data) {

        d = {
            type: options.type,
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

        window[options.name] = new Chart(document.getElementById(options.id).getContext("2d"), d);
    }

    $.ajax({
        url: options.url,
        success: onSuccess
    })
}

function makeLineChart(options) {

    function onSuccess(data) {
        s = {
            type: options.type,
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

        var pie = document.getElementById(options.id).getContext("2d");

        window[options.name] = new Chart(pie, s);
    }

    $.ajax({
        url: options.url,
        success: onSuccess
    })
}