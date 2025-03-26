DOMAINS
    int_list = integer*

PREDICATES
    nondeterm read_list(integer, int_list)
    nondeterm minus(int_list, int_list, int_list)
    nondeterm symmetry(int_list, int_list, int_list)
    nondeterm member(integer, int_list)
    nondeterm two_to_one_list(int_list, int_list, int_list)

CLAUSES
    read_list(0, []) :- !.
    read_list(N, [H|T]) :-
        write("Enter element ", N, ": "),
        readint(H),
        N1 = N - 1,
        read_list(N1, T).


    member(H, [H|_]).
    member(H, [_|T]) :- member(H, T).


    two_to_one_list([], L, L).
    two_to_one_list([H|T], L, [H|Result]) :- two_to_one_list(T, L, Result).

 %элементы из первого списка, которых нет во втором
    minus([], _, []).
    minus([H|T], L, Result) :-
        member(H, L),
        !,
        minus(T, L, Result).
    minus([H|T], L, [H|Result]) :-
        minus(T, L, Result).


    symmetry(A, B, SM) :-
        minus(A, B, A_B),
        minus(B, A, B_A),
        two_to_one_list(A_B, B_A, SM).

GOAL
    write("Count of elements in list A (>0): "),
    readint(N_A),
    read_list(N_A, A),
    write("Count of elements in list B (>0): "),
    readint(N_B),
    read_list(N_B, B),
    symmetry(A, B, SM),
    write("Symmetric difference: ", SM), nl.