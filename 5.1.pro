DOMAINS
    digit_list = integer*

PREDICATES
    nondeterm number_to_digits(integer, digit_list, digit_list)

CLAUSES
    number_to_digits(0, Acc, Acc).
    number_to_digits(N, Acc, Digits) :-
        N > 0,
        D = N mod 10,
        N1 = N div 10,
        number_to_digits(N1, [D|Acc], Digits).

GOAL
    write("Enter natural number N: "),
    readint(N),
    number_to_digits(N, [], Digits),
    write("Digits of N: ", Digits), nl.
