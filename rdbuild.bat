for /f "tokens=*" %%a in ('dir obj /b /ad /s ^|sort') do rd "%%a" /s/q

for /f "tokens=*" %%a in ('dir Release /b /ad /s ^|sort') do rd "%%a" /s/q

for /f "tokens=*" %%a in ('dir Debug /b /ad /s ^|sort') do rd "%%a" /s/q


pause