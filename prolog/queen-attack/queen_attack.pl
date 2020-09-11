on_board(X):- X > 0, X < 8.

create((X,Y)):- on_board(X), on_board(Y), assertz(pos(X, Y)).

attack((X, _), (X, _)):- true, !.
attack((_, Y), (_, Y)):- true, !.
attack((X1, Y1), (X2, Y2)):- abs(X1 - X2) =:= abs(Y1 - Y2).
