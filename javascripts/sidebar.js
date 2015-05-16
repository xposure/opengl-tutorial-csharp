$(function () {
    var sidebar = $(".bs-docs-sidenav");
    sidebar.empty();

    var r = /[a-z0-9]/gi;
    function cleanString(text) {
        var newText = "";
        for (var i = 0 ; i < text.length; i++) {
            if (text[i].match(r))
                newText += text[i];
            else
                newText += "-";
        }

        return newText.toLowerCase();
    }

    function createEntry(depth, header, sidebar) {

        var text = header.text();
        var clean = cleanString(text);
        //<h1 id="home" class="page-header">Home<a class="anchorjs-link" href="#home"><span class="anchorjs-icon"></span></a></h1>

        header.attr("id", clean);
        header.append("<a class='anchorjs-link' href='#" + clean + "'><span class='anchorjs-icon'></span></a>");

        var navli = $("<li></li>");
        var nava = $("<a href='#" + clean + "'>" + text + "</a>");

        navli.append(nava);
        sidebar.append(navli);

        depth++;
        if (depth < 5) {
            var next = header.next();
            var ul = $("<ul class='nav'></ul>");
            var added = false;
            next.find(" > h" + depth).each(function (it) {
                if (!added) {
                    navli.append(ul);
                    added = true;
                }

                createEntry(depth, $(this), ul);
            });
        }
    }

    $(".bs-docs-section > h1").each(function (it) {
        var $this = $(this);
        createEntry(1, $this, sidebar);
    });

    var naxSidebar = $(".bs-docs-sidebar");
    var sidebarTop = naxSidebar.offset().top;
    $(window).scroll(function () {
        var top = $(window).scrollTop();
        if (top > sidebarTop) {
            if (naxSidebar.hasClass("affix-top")) {
                naxSidebar.removeClass("affix-top").addClass("affix");
            }
        } else {
            if (naxSidebar.hasClass("affix")) {
                naxSidebar.removeClass("affix").addClass("affix-top");
            }
        }
    });

    $('body').scrollspy({
        target: '.bs-docs-sidebar',
        offset: 40
    });
});