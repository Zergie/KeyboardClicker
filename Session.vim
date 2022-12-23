let SessionLoad = 1
let s:so_save = &g:so | let s:siso_save = &g:siso | setg so=0 siso=0 | setl so=-1 siso=-1
let v:this_session=expand("<sfile>:p")
silent only
silent tabonly
cd C:/GIT/KeyboardClicker
if expand('%') == '' && !&modified && line('$') <= 1 && getline(1) == ''
  let s:wipebuf = bufnr('%')
endif
let s:shortmess_save = &shortmess
if &shortmess =~ 'A'
  set shortmess=aoOA
else
  set shortmess=aoO
endif
badd +1 Program.cs
badd +40 Form1.Designer.cs
badd +47 NativeMethods.cs
badd +43 Form1.cs
badd +1 /$metadata$/Project/KeyboardClicker/Assembly/System/Drawing/Common/Symbol/System/Drawing/Graphics.cs
badd +1 /$metadata$/Project/KeyboardClicker/Assembly/System/Windows/Forms/Symbol/System/Windows/Forms/Control.cs
badd +1 Shapes/IShape.cs
badd +2 Rectangle.cs
badd +1 Shapes/Shape.cs
badd +1 HintGenerators/ColemakDhHintGenerator.cs
badd +23 HintGenerators/HintGenerator.cs
badd +1 Shapes/RectangleShape.cs
badd +5 ShapeGenerators/Shape.cs
badd +24 ShapeGenerators/ShapeGenerator.cs
badd +74 .editorconfig
argglobal
%argdel
edit Form1.cs
let s:save_splitbelow = &splitbelow
let s:save_splitright = &splitright
set splitbelow splitright
wincmd _ | wincmd |
vsplit
1wincmd h
wincmd w
let &splitbelow = s:save_splitbelow
let &splitright = s:save_splitright
wincmd t
let s:save_winminheight = &winminheight
let s:save_winminwidth = &winminwidth
set winminheight=0
set winheight=1
set winminwidth=0
set winwidth=1
exe 'vert 1resize ' . ((&columns * 114 + 118) / 236)
exe 'vert 2resize ' . ((&columns * 121 + 118) / 236)
argglobal
balt NativeMethods.cs
setlocal fdm=manual
setlocal fde=0
setlocal fmr={{{,}}}
setlocal fdi=#
setlocal fdl=0
setlocal fml=1
setlocal fdn=20
setlocal fen
silent! normal! zE
let &fdl = &fdl
let s:l = 43 - ((34 * winheight(0) + 30) / 61)
if s:l < 1 | let s:l = 1 | endif
keepjumps exe s:l
normal! zt
keepjumps 43
normal! 013|
wincmd w
argglobal
if bufexists(fnamemodify("ShapeGenerators/ShapeGenerator.cs", ":p")) | buffer ShapeGenerators/ShapeGenerator.cs | else | edit ShapeGenerators/ShapeGenerator.cs | endif
if &buftype ==# 'terminal'
  silent file ShapeGenerators/ShapeGenerator.cs
endif
balt /$metadata$/Project/KeyboardClicker/Assembly/System/Drawing/Common/Symbol/System/Drawing/Graphics.cs
setlocal fdm=manual
setlocal fde=0
setlocal fmr={{{,}}}
setlocal fdi=#
setlocal fdl=0
setlocal fml=1
setlocal fdn=20
setlocal fen
silent! normal! zE
let &fdl = &fdl
let s:l = 80 - ((25 * winheight(0) + 30) / 61)
if s:l < 1 | let s:l = 1 | endif
keepjumps exe s:l
normal! zt
keepjumps 80
normal! 0
lcd C:/GIT/KeyboardClicker
wincmd w
exe 'vert 1resize ' . ((&columns * 114 + 118) / 236)
exe 'vert 2resize ' . ((&columns * 121 + 118) / 236)
tabnext 1
if exists('s:wipebuf') && len(win_findbuf(s:wipebuf)) == 0 && getbufvar(s:wipebuf, '&buftype') isnot# 'terminal'
  silent exe 'bwipe ' . s:wipebuf
endif
unlet! s:wipebuf
set winheight=1 winwidth=20
let &shortmess = s:shortmess_save
let &winminheight = s:save_winminheight
let &winminwidth = s:save_winminwidth
let s:sx = expand("<sfile>:p:r")."x.vim"
if filereadable(s:sx)
  exe "source " . fnameescape(s:sx)
endif
let &g:so = s:so_save | let &g:siso = s:siso_save
set hlsearch
doautoall SessionLoadPost
unlet SessionLoad
" vim: set ft=vim :
