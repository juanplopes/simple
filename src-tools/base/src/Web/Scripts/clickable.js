(function($) {
	$.fn.extend( {
		clickable: function( cpTtl ) { // v0.1.9
			return this
				.each( function() {
					var jClickElem = $(this);
					var jGuideLink = $("a[href]:not([href='#']):not([href^='javascript:'])", jClickElem).eq(0);
					if ( !jGuideLink ) return true;	// continue
					
					var href = jGuideLink.attr("href");
					if ( !href ) return true;	// continue
					
					$(this).data("href", href);
					if ( ( cpTtl || cpTtl == null ) && !this.title && jGuideLink[0].title ) this.title = jGuideLink[0].title;
					jClickElem
						.click( function() { window.location.href = $(this).data("href"); } )
						.hover(
							function() { $(this).addClass("jsClickableHover"); },
							function() { $(this).removeClass("jsClickableHover"); }
						)
						.addClass("jsClickable");
						
					jGuideLink
						.focus( function() { $(this).parents(".jsClickable").eq(0).addClass("jsClickableFocus"); })
						.blur( function() { $(this).parents(".jsClickable").eq(0).removeClass("jsClickableFocus"); })
						.addClass("jsGuide");
				} );
		}
	} );
} )(jQuery);