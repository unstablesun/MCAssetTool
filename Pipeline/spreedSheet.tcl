namespace eval spreadsheet {
    proc create {w args} {
        upvar #0 $w a
        array set a {-rows 3 -cols 2 -width 8} ;# default parameters
        array set a $args                      ;# maybe override defaults
        set a(last) $a(-rows),$a(-cols)        ;# keep grand total field
        trace var a w spreadsheet::redo
        frame $w
        for {set y 0} {$y<=$a(-rows)} {incr y} {
            set row [list]
            for {set x 0} {$x<=$a(-cols)} {incr x} {
                set e [entry $w.$y,$x -width $a(-width) \
                    -textvar ::$w\($y,$x) -just right]
                if {$x==$a(-cols) || $y==$a(-rows)} {
                    $e config -state disabled -background grey -relief flat
                }
                lappend row $e
            }
            eval grid $row -sticky news
        }
        $e config -relief solid
        set w
    }
    proc redo {_var el op} {
        upvar #0 $_var a
        if {$el!=$a(last)} {
            foreach {y x} [split $el ,] break
            if {$x!=""} {
                sum $_var $y,* $y,$a(-cols)
                sum $_var *,$x $a(-rows),$x                
            } ;# otherwise 'el' was not a cell index
        }     ;# prevent endless recalculation of grand total
    }
    proc sum {_var pat target} {
        upvar #0 $_var a
        set sum 0
        set it "" ;# default if no addition succeeds
        foreach i [array names a $pat] {
            if {$i!=$target} {
                catch {set it [set sum [expr {$sum+$a($i)}]]}
            } ;# don't pull the total into the total
        }
        set ::$_var\($target) $it
    }
}
# test and usage example (NB: initial values can be set at creation time)
pack [spreadsheet::create .s 0,0 Header 1,0 47 2,1 11] -fill both