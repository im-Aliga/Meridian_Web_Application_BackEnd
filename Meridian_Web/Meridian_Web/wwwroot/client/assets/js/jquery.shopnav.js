
;(function ($, window, document, undefined) {
    'use strict';

    $.fn.shopNav = function (options) {

        var defaults = {
            mobileBreakpoint: 1024
        },
       
        settings = $.extend({}, defaults, options),
        bigScreenFlag = Number.MAX_VALUE,
        smallScreenFlag = 1,
        $element = this,
        mode,
        tabInit = false,
        $toggleBtn = $element.find('.toggle-button'),
        $closeSpan = $element.find('.ah-close'),
        $underneathSpanToggle = $element.find('.has-dropdown').children('.js-menu-toggle'),
        $megaBtn,$megaMenuItem,$megaMenuSpan,$megaMenuContent;


    return this.each(function() {

      
        var windowWidth = function () {
            return window.innerWidth || document.documentElement.clientWidth
                || document.body.clientWidth;
        };
       
        var checkTabs = function () {
            if ($element.find('.mega-menu').length > 0) {
                tabInit = true;
             
                $megaBtn = $('.mega-text');
           
                $megaMenuItem = $element.find('.mega-menu-list').find('li');
               
                 $megaMenuSpan =  $element.find('.mega-menu-list').find('span');
               
                 $megaMenuContent = $element.find('.mega-menu-content');
            }
        };
        
        var globalBodyClick = function () {
            if ($megaBtn.hasClass('js-open')) {
              $megaBtn.removeClass('js-open');
            }
        };
      
        var clickMegaBtn = function (e) {
           
            e.stopPropagation();
            $megaBtn.toggleClass('js-open');
        };
       
        var bindEventsMegaMenu = function () {
            if (tabInit) {
                if (mode === 'landscape') {
                    $megaBtn.on('click',clickMegaBtn);
                    $(document.body).on('click',globalBodyClick);
                    $megaMenuItem.on('mouseenter',mouseEnterMegaItem);
                } else {
                    $megaBtn.off('click');
                    $megaBtn.removeClass('js-open');
                    $(document.body).off('click',globalBodyClick);
                    $megaMenuSpan.on('click',clickMegaSpan);
                    $megaMenuItem.off('mouseenter').off('mouseleave')
                }
            }
        };
        
        var mouseEnterMegaItem = function () {
           
            $megaMenuItem.removeClass('js-active');
            $megaMenuSpan.removeClass('js-toggle-mark');
            $(this).addClass('js-active');
            $(this).find('span').addClass('js-toggle-mark');
            $megaMenuContent.removeClass('js-active');
            $($megaMenuContent[$(this).index()]).addClass('js-active');
        };
       
        var clickMegaSpan = function () {
            $megaMenuSpan.removeClass('js-toggle-mark');
            $megaMenuItem.removeClass('js-active');
            $(this).addClass('js-toggle-mark');
            $(this).parent().addClass('js-active');
            $megaMenuContent.removeClass('js-active');
            $($megaMenuContent[$(this).parent().index()]).addClass('js-active');
        };
       
        var attachClickOnToggleBtn = function () {
            $toggleBtn.on('click',function () {
                $element.addClass('js-open');
            });
        };
       
        var attachClickOnCloseSpan = function () {
            $closeSpan.on('click',function () {
                $element.removeClass('js-open');
            });
        };

       
        var attachClickOnUnderneathSpan = function () {
            $underneathSpanToggle.on('click',function () {
                $(this).toggleClass('js-toggle-mark');
                $(this).next().stop(true,true).slideToggle(300);
            });
        };

     
          var flushPluginState = function () {
           
            $element.removeClass('js-open');
            $underneathSpanToggle.removeClass('js-toggle-mark');
            $underneathSpanToggle.next().css('display','');
        };



      
        var windowCheck = function() {
            
            if(windowWidth() <= settings.mobileBreakpoint && bigScreenFlag > settings.mobileBreakpoint){
                mode = 'portrait';
                bindEventsMegaMenu();
            }
            
            if(windowWidth() > settings.mobileBreakpoint && smallScreenFlag <= settings.mobileBreakpoint) {
                mode = 'landscape';
                bindEventsMegaMenu();
                flushPluginState();
            }
            bigScreenFlag = windowWidth();
            smallScreenFlag = windowWidth();
        };




      
        checkTabs();
        attachClickOnToggleBtn();
        attachClickOnCloseSpan();
        attachClickOnUnderneathSpan();
        windowCheck();

        $(window).on('resize', function() {
            windowCheck();
        });

    });
    };
})(jQuery, window, document);
