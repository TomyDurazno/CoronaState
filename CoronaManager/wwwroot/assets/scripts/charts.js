//READY
$(() => {

    var pies = $('[data-type="pie"]');

    pies.map((i, d) => makeOptions(d))
        .map((i, d) => makePieChart(d));    

    var lines = $('[data-type="line"]');

    lines.map((i, d) => makeOptions(d))
         .map((i, d) => makeLineChart(d)); 

    var bars = $('[data-type="bar"]');

    bars.map((i, d) => makeOptions(d))
        .map((i, d) => makeBarChart(d)); 

    var tabbuttons = $('[data-tabpanel]');

    tabbuttons.map((i, tab) => $(tab).on("click", function () { tabButtonOnClick(tab, tabbuttons) }));
})

function tabButtonOnClick(tab, tabbuttons) {

    var others = tabbuttons.filter((i, t) => t.id !== tab.id);

    others.map((i, t) => $(t).removeClass("active"));

    $(tab).addClass("active");

    var otherpanels = $(".panel").filter((i, p) => p.id !== tab.dataset.tabpanel);

    otherpanels.map((i, p) => $(p).hide());

    var myPanel = $(".panel").filter((i, p) => p.id === tab.dataset.tabpanel);

    myPanel.map((i, p) => $(p).attr("hidden", false).fadeIn());
}

function makeOptions(d) {
    return {
        name: d.dataset.name,
        type: d.dataset.type,
        id: d.id,
        url: d.dataset.url,
        data: {
            continent: d.dataset.continent
        }
    }
}

function makePieChart(options) {

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
        data: options.data,
        success: onSuccess
    });
}

function makeBarChart(options) {

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
        data: options.data,
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
        data: options.data,
        success: onSuccess
    })
}