##+##########################################################################
#
# challenger.tcl
#
# Challenger -- helps solve the challenger math puzzle
# by Keith Vetter
#
# Revisions:
# KPV Oct 08, 2002 - initial revision
# KPV Oct 14, 2002 - added auto solve
#
##+##########################################################################

package require Tk


# All the cells in the puzzle
set state(cells) {      0 4
   1 0  1 1  1 2  1 3  1 4
   2 0  2 1  2 2  2 3  2 4
   3 0  3 1  3 2  3 3  3 4
   4 0  4 1  4 2  4 3  4 4
   5 0  5 1  5 2  5 3  5 4 }

# Some puzzles to play with
set puzzle(0) {16 . 1 . . 9 . . . 3 16 . . 2 . 17 1 . . . 9 11 18 12 10 12}
set puzzle(1) {24 . . 3 . 14 . 9 . . 34 . . . 7 33 6 . . . 30 28 27 29 27 32}
set puzzle(2) {18 . . 3 . 10 . 4 . . 20 . . . 1 10 5 . . . 20 12 13 17 18 12}
set puzzle(3) {7 . 1 . . 8 . . . 1 9 . . 1 . 10 1 . . . 11 9 9 9 11 12}
set puzzle(4) {23 . . . 5 23 6 . . . 23 . . 6 . 23 . 5 . . 23 25 19 25 23 23}
set puzzle(5) {33 . 9 . . 28 . . . 6 30 . . 8 . 29 8 . . . 30 29 30 32 26 27}
set puzzle(6) {35 . . 5 . 23 . 2 . . 21 . . . 9 20 8 . . . 31 20 24 20 31 17}
set puzzle(7) {28 . 9 . . 26 . . 5 . 26 8 . . . 27 . . . 5 27 30 30 22 24 25}
set puzzle(8) {15 5 . . . 16 . . . 3 6 . 3 . . 22 . . 4 . 15 21 12 7 19 9}
set puzzle(9) {23 . . . 9 19 . 4 . . 11 7 . . . 15 . . 8 . 35 22 18 20 20 21}
set puzzle(10) {9 2 . . .  8 . . 4 . 10 . . . 2 11 . 3 . . 15 10  6 14 14 16}
set puzzle(11) {29 . . 8 . 30 2 . . . 22 . 6 . . 32 . . . 9 31 25 27 32 31 33}

# The rows, columns and diagonals
array set rows {
   1,4 {1,0 1,1 1,2 1,3}  2,4 {2,0 2,1 2,2 2,3}  3,4 {3,0 3,1 3,2 3,3}
   4,4 {4,0 4,1 4,2 4,3}  5,0 {1,0 2,0 3,0 4,0}  5,1 {1,1 2,1 3,1 4,1}
   5,2 {1,2 2,2 3,2 4,2}  5,3 {1,3 2,3 3,3 4,3}  5,4 {1,0 2,1 3,2 4,3}
   0,4 {4,0 3,1 2,2 1,3}
}
array set move {Up {-1 0} Down {1 0} Left {0 -1} Right {0 1}}

set state(locked) 0
set state(undo) {}
set state(forced) {}
set state(who) -1

proc DoDisplay {} {
   wm title . "TkChallenger"
   DoMenus

   frame .play -bd 2 -relief raised -padx 30 -pady 0
   frame .play.tm -height 20
   grid .play.tm
   frame .bottom
   for {set row 0} {$row < 6} {incr row} {
       set cells {}
       if {$row == 0} {set cells "x x x x"}
       for {set col 0} {$col < 5} {incr col} {
           if {$row == 0} {set col 4}
           set tag ".e$row,$col"
           entry $tag -width 6 -textvariable ss($row,$col) -justify c \
               -disabledbackground lightblue -exportselection 0
           $tag config -disabledforeground [$tag cget -foreground]
           bind $tag <Key> [list MyKey %W %A %K]
           if {$row == 5 || $col == 4} {
               $tag config -bg cyan -disabledbackground cyan
           }
           lappend cells $tag
       }
       eval grid $cells -in .play -sticky we
   }
   label .msg -anchor w -bg [.play cget -bg] -textvariable state(msg)
   grid .msg - - - - -in .play -sticky ew -pady 5
   button .forced -text "Do Forced Moves" -command DoForced -takefocus 0
   button .undo -text Undo -command Undo -state disabled -takefocus 0
   button .solve -text Solve -command Solve -takefocus 0

   pack .play .bottom -side top -fill both -expand 1
   pack .solve .forced .undo -in .bottom -side left -pady 10 -expand 1

   array set ::b2m {.lock {.m.puzzle 4} .unlock {.m.puzzle 5} .undo {.m.edit 4}
       .erase {.m.edit 0} .eraseA {.m.edit 1} .forced {.m.edit 3}
       .solve {.m.puzzle 7}
   }
   focus .e1,0
   DoButtons
}
proc DoMenus {} {
   menu .m -tearoff 0
   . configure -menu .m                 ;# Attach menu to main window

   # Top level menu buttons
   .m add cascade -menu .m.puzzle -label "Puzzle" -underline 0
   .m add cascade -menu .m.edit   -label "Edit"   -underline 0
   .m add cascade -menu .m.help   -label "Help"   -underline 0

   menu .m.puzzle -tearoff 0
   .m.puzzle add command -label "Reset Puzzle" -under 0 \
       -command {PickPuzzle -1}
   .m.puzzle add command -label "Blank Puzzle" -under 0 -command {Erase 1}
   .m.puzzle add command -label "New Puzzle" -under 0 -command PickPuzzle
   .m.puzzle add separator
   .m.puzzle add command -label "Lock Puzzle" -under 0 -command Lock
   .m.puzzle add command -label "Unlock Puzzle" -under 0 -command Unlock
   .m.puzzle add separator
   .m.puzzle add command -label Solve -under 0 -command Solve
   .m.puzzle add separator
   .m.puzzle add command -label Exit -under 0 -command exit

   menu .m.edit -tearoff 0
   .m.edit add command -label "Erase" -under 0 -command {Erase 0}
   .m.edit add command -label "Erase All" -under 6 -command {Erase 1}
   .m.edit add separator
   .m.edit add command -label "Do Forced Moves" -under 0 -command DoForced
   .m.edit add command -label "Undo"            -under 0 -command Undo

   menu .m.help -tearoff 0
   .m.help add command -label Help  -under 0 -command Help
}
proc INFO {msg} { set ::state(msg) $msg ; update}

proc PickPuzzle {{who ""}} {
   global state ss puzzle

   if {$who == ""} {                           ;# Pick one at random
       set names [array names puzzle]
       set len [llength $names]
       while {1} {
           set n [expr {int(rand() * $len)}]
           set who [lindex $names $n]
           if {$who != $state(who)} break
           if {$len == 1} break
       }
   } elseif {$who == -1} {
       set who $state(who)
   }
   if {! [info exists puzzle($who)]} {
       Erase 0
       return
   }
   Erase 1
   set state(who) $who

   foreach {row col} $state(cells) val $puzzle($who) {
       if {$val == "."} {set val {}}
       set ss($row,$col) $val
   }
   Lock
   INFO "Puzzle #$who"
}
# DoButtons -- set the buttons state depending on circumstances
proc DoButtons {} {
   global state b2m

   array set s {1 normal 0 disabled}
   set ww [list .lock .unlock .erase .eraseA .solve .undo .forced]

   # Get into bb the states we want
   if {$state(locked)} { set bb {0 1 1 0 1} } { set bb {1 0 0 1 0} }
   lappend bb [expr {[llength $state(undo)]   > 0 ? 1 : 0}]
   lappend bb [expr {[llength $state(forced)] > 0 ? 1 : 0}]

   foreach w $ww b $bb {
       if {[winfo exists $w]} {                ;# Configure the button
           $w configure -state $s($b)
       }
       foreach {m e} $::b2m($w) {              ;# Configure the menu
           $m entryconfigure $e -state $s($b)
       }
   }
}
# Lock -- locks (by disabling) all cells w/ values in them
proc Lock {} {
   global ss state

   foreach {row col} $state(cells) {
       set tag ".e$row,$col"
       if {$row == 5 || $col == 4} {
           append ss($row,$col) "/  "
       } elseif {$ss($row,$col) == ""} continue
       $tag config -state disabled
   }
   set state(locked) 1
   set state(undo) {}
   DoButtons
   SumRows
   set w [focus -lastfor .]

   catch {
       if {[$w cget -state] != "normal"} {
           event generate $w <Tab>
       }
   }
}
# Unlock -- unlocks (by enabling) all cells
proc Unlock {} {
   global ss state

   foreach {row col} $state(cells) {
       set tag ".e$row,$col"
       $tag config -state normal

       if {$row == 5 || $col == 4} {
           regsub {/.*} $ss($row,$col) {} ss($row,$col)
       }
   }
   set state(locked) 0
   DoButtons
}
# Erase -- erases either all non-locked cells, or all cells
proc Erase {all} {
   if {! $all && $::state(locked) == 0} return

   set undo {}
   foreach {row col} $::state(cells) {
       set tag ".e$row,$col"
       if {$all || [$tag cget -state] == "normal"} {
           set was [$tag get]
           lappend undo $tag "$row,$col" $was
           set ::ss($row,$col) ""
       }
   }
   SumRows
   lappend ::state(undo) $undo
   if {$all} {
       INFO ""
       Unlock
       set ::state(who) "user"
   }
   focus .e1,0
   DoButtons
}
proc GetPuzzle {} {
   global state ss

   set p ""
   foreach {row col} $state(cells) {
       set val $ss($row,$col)
       if {$val == ""} {set val .}
       regsub {/.*} $val "" val
       append p "$val "
   }
   return [string trim $p]
}

# SumRows -- the workhorse of our program. Sums up each row and
# updates the running total (deficit actually) and does cell
# configuring for bad, good and forced cells.
proc SumRows {} {
   global state ss

   if {$::state(locked) == 0} { return 0}

   foreach {row col} $state(cells) {           ;# Put all cells back to white
       set tag ".e$row,$col"
       if {[$tag cget -state] == "normal"} { $tag config -bg white }
       if {$row == 5 || $col == 4} { $tag config -disabledbackground cyan }
   }

   _SumRows ss

   foreach cell $state(good) {
       .e$cell config -disabledbackground green
   }
   foreach cell $state(bad) {
       .e$cell config -disabledbackground red
   }
   foreach {cell value} $state(forced) {
       .e$cell config -background yellow
   }
   DoButtons
   if {[llength $state(good)] == 10} { return 1 } ;# Solved
   if {[llength $state(bad)] != 0} { return -1 } ;# Bad
   return 0
}
proc _SumRows {_SS} {
   global rows state
   upvar 1 $_SS SS


   set state(forced) {}
   set state(bad) {}
   set state(good) {}

   foreach scell [array names rows] {          ;# Loop on each row/col/diagonal

       set n [regexp {\s*([0-9]+)/?} $SS($scell) => max]
       if {! $n} continue
       set sum 0
       set missing {}
       foreach cell $rows($scell) {            ;# Each cell in row/col/diag
           set val $SS($cell)
           if {[string is integer -strict $val]} {
               set sum [expr {$sum + $val}]
           } else {
               lappend missing $cell
           }
       }
       # Show running deficit
       set SS($scell) [format "%2d/%2d" $max [expr {$max - $sum}]]
       set SS(d,$scell) [expr {$max - $sum}]

       # Figure out bad, good or forced cells stuff
       set num [llength $missing]
       if {$num == 0 && $sum == $max} {
           lappend state(good) $scell
       } elseif {$num == 0 || $sum > $max} {
           lappend state(bad) $scell
       } else {
           set delta [expr {1.0 * ($max - $sum) / $num}]
           if {$delta < 1 || $delta > 9} {
               lappend state(bad) $scell
           } elseif {$num == 1 || $delta == 1.0 || $delta == 9.0} {
               foreach who $missing {
                   lappend state(forced) $who [expr {int($delta)}]
               }
           }
       }
   }
}
proc IsSolved {} {
   return [expr {[llength $::state(good)] == 10 ? 1 : 0}]
}
# DoForced -- fills in the values for all forced cells
proc DoForced {{repeat 0}} {
   global state ss
   set undo {}                                 ;# So we can undo this action

   while {[llength $state(forced)] > 0} {
       catch {unset done}
       foreach {cell val} $state(forced) {
           if {[info exists done($cell)]} continue
           lappend undo ".e$cell" $cell $ss($cell)
           set ss($cell) $val
           set done($cell) $val
       }
       lappend state(undo) $undo
       SumRows
       if {! $repeat} break
   }
}
proc _DoForced {_SS} {
   global state
   upvar 1 $_SS SS

   _SumRows SS
   while {[llength $state(forced)] > 0} {
       foreach {cell val} $state(forced) {
           set SS($cell) $val
       }
       _SumRows SS
       if {[llength $state(bad)] > 0} { return -1 }
   }
   if {[llength $state(good)] == 10} { return 1 } ;# Solved
   if {[llength $state(bad)] != 0} { return -1 } ;# Bad
   return 0
}
# MyKey -- handles all keystrokes for each cell
proc MyKey {w char sym} {
   regexp {([0-9]),([0-9])} $w who row col
   set before [$w get]                         ;# For undo info

   switch -- $sym {
       "Tab" { return -code continue }
       "asterisk" { Undo }
       z { if {$char == "\x1A"} Undo }
       "space" { $w delete 0 end }
       "BackSpace" - "Delete" {
           set pos [$w index insert]
           $w delete [incr pos -1] end
       }
       "Home" - "End" {
           if {$sym == "End"} {set event <Shift-Tab>} {set event <Tab>}
           focus .e0,4 ; event generate .e0,4 $event
       }
       "Up" - "Down" - "Left" - "Right" {
           foreach {drow dcol} $::move($sym) break
           while {1} {
               incr row $drow ; incr col $dcol
               set ww ".e$row,$col"
               if {! [winfo exists $ww]} {return -code break}
               if {[$ww cget -state] != "normal"} continue
               focus $ww
               break
           }
       }
       default {
           if {! [string is integer -strict $char]} { return -code break }
           if {$row == 5 || $col == 4} {               ;# Sum cells
               set val [$w get]
               if {$val != ""} {
                   set char [expr {(($val * 10) + $char) % 100}]
               }
           } elseif {$char == "0"} {
               set char ""
           }
           $w delete 0 end
           $w insert 0 $char
           #$w selection range 0 end
           $w icursor end
           if {$row < 5 && $col < 4} {
               event generate $w <Tab>         ;# Move to next cell
           }
       }
   }
   set now [$w get]
   if {$before != $now} {lappend ::state(undo) [list $w $who $before]}
   SumRows
   return -code break
}
# Undo -- Undoes the last operation
proc Undo {} {
   global state ss

   # Pop off the event to undo
   set item [lindex $state(undo) end]
   set state(undo) [lrange $state(undo) 0 end-1]

   foreach {w who was} $item {
       set ss($who) $was
       focus $w
   }
   SumRows
   DoButtons

}
proc Help {} {
   catch {destroy .help}
   toplevel .help
   wm title .help "TkChallenger Help"
   wm geom .help "+[expr {[winfo x .] + [winfo width .] + 10}]+[winfo y .]"

   text .help.t -relief raised -wrap word -width 70 -height 34
   .help.t config -padx 10 -pady 10
   button .help.dismiss -text Dismiss -command {destroy .help}

   pack .help.t -side top -expand 1 -fill both
   pack .help.dismiss -side bottom -expand 1 -pady 10

   set bold "[font actual [.help.t cget -font]] -weight bold"
   .help.t tag configure title -justify center -foreground red \
       -font "Times 20 bold"
   .help.t tag configure title2 -justify center -font "Times 12 bold"
   .help.t tag configure bullet -font $bold
   .help.t tag configure bn -lmargin1 15 -lmargin2 15
   .help.t tag configure bn2 -lmargin1 15 -lmargin2 20

   .help.t insert end "TkChallenger\n" title
   .help.t insert end "by Keith Vetter\n\n" title2

   set m "This program helps you solve the Challenger puzzles that you often "
   append m "see in the daily paper. I wrote this program because I'm "
   append m "horrible at solving these puzzles--this helps but I'm still bad."
   .help.t insert end $m n \n\n

   .help.t insert end "How to Play" bullet \n
   set m "Fill in each square with a number, 1-9. "
   append m "Horizontal square should add to the total on "
   append m "the right, vertical squares to the number on the "
   append m "bottom and the main diagonals to the number in "
   append m "the upper and lower right."
   .help.t insert end $m bn \n\n

   .help.t insert end "What the Different Colored Squares Mean" bullet \n
   set m "- Cyan squares show the value each row or column must add up "
   append m "to along with the amount still needed to reach that value.\n"
   append m "- Light blue squares are playing squares with known values.\n"
   append m "- Green squares show when a row or column is correct.\n"
   append m "- Red squares show when a row or column is in an illegal state.\n"
   append m "- Yellow squares show squares for which the value is forced."
   .help.t insert end $m bn2 \n\n

   .help.t insert end "Built in Puzzles" bullet \n
   set m "TkChallenger comes with about half-a-dozen built in puzzles, "
   append m "but is really designed for you to create your own. "
   append m "Unlocking the puzzle allows you to enter values into "
   append m "any square. Once you've entered the puzzle, lock it and "
   append m "solve away."
   .help.t insert end $m bn \n\n

   .help.t insert end "Auto Solve" bullet \n
   set m "TkChallenge can solve the puzzle for you. It does a brute "
   append m "force search trying all possibilities for the top two "
   append m "rows, after which the remaining squares are usually forced. "
   append m "This requires at most 6,561 (9^4) guesses for the typical "
   append m "published Challenger puzzle that has 4 squares already filled "
   append m "in."
   .help.t insert end $m bn \n\n

   .help.t config -state disabled
}
proc Solve {} {
   INFO "searching for solution..."
   DoForced                                    ;# Fill all forced cells
   set start [clock click -milliseconds]
   set code [GenerateCode]                     ;# This code will solve it
   eval $code
   foreach {solved cnt} [solvex] break
   
   set start [expr {([clock click -milliseconds] - $start)/1000.0}]
   set guesses "guess" ; if {$cnt != 1} {set guesses guesses}
   if {$solved} {
       INFO "Solved: $start sec and $cnt $guesses"
       set ::state(undo) {}
   } else {
       INFO "No solution: $start sec and $cnt $guesses"
   }
   DoButtons
}
proc GenerateCode {} {
   set braces 0
   set code "proc solvex {} \{\n set cnt 0\n"
   append code " array set SS \[array get ::ss]\n\n"

   set indent 1
   foreach row {1 2} {
       foreach {b code2} [GenCodeRow $row [string repeat " " $indent]] {
           incr braces $b
           append code $code2 "\n"
           incr indent $b
       }
   }
   set ind [string repeat " " $indent]

   append code $ind "set save \[array get SS]\n"
   append code $ind "incr cnt\n"
   append code $ind "set n \[_DoForced SS]\n"
   append code $ind "if {\$n == 1} \{\n"
   append code $ind " array set ::ss \[array get SS]\n"
   append code $ind " SumRows\n"
   append code $ind " return \[list 1 \$cnt]\n"
   append code $ind "\}\n"
   append code $ind "array set SS \$save\n"
   append code [string repeat "\}\n" $braces]
   append code " return \[list 0 \$cnt]\n"
   append code "\}"

   return $code
}
proc GenCodeRow {row indent} {
   global ss

   set missing {}
   foreach col {0 1 2 3} {
       if {$ss($row,$col) == {}} { lappend missing "SS($row,$col)" }
   }
   set num [llength $missing]
   if {$num == 0} {
       return [list 0 "$indent; # complete row\n"]
   }

   set last [lindex $missing end]
   set code ""
   set code2 "set $last \[expr {$ss(d,$row,4)"
   for {set i 0} {$i < $num-1} {incr i} {
       set cell [lindex $missing $i]
       append code $indent
       append indent " "
       append code "for {set $cell 1} {\$$cell < 10} {incr $cell} \{\n"
       append code2 " - \$$cell"
   }
   append code $indent $code2 "}]\n"
   append code $indent "if {\$$last < 1 || \$$last > 9} continue\n"
   return [list [incr num -1] $code]
}

DoDisplay
PickPuzzle