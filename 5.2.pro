DOMAINS
    int_list = integer*

PREDICATES
    nondeterm read_list(integer, int_list)
    nondeterm count_min(int_list, integer, integer, integer)
    nondeterm find_min(int_list, integer)
    nondeterm read_count(integer)

CLAUSES
    
    read_count(N) :-
        write("Count of elements in list(>0): "),
        readint(N).

    
    read_list(0, []) :- !. 
    read_list(N, [H|T]) :-
        write("Enter element ", N, ": "),
        readint(H),
        N1 = N - 1,
        read_list(N1, T).

   
    count_min([], Min, Count, Count) :- !.
    count_min([H|T], Current_Min, Current_Count, Result) :-
        H < Current_Min, !,
        count_min(T, H, 1, Result).
    count_min([H|T], Current_Min, Current_Count, Result) :-
        H = Current_Min, !,
        New_Count = Current_Count + 1,
        count_min(T, Current_Min, New_Count, Result).
    count_min([_|T], Current_Min, Current_Count, Result) :-
        count_min(T, Current_Min, Current_Count, Result).

   
    find_min([H|T], Result) :-
        count_min(T, H, 1, Result).

GOAL
    read_count(N),     
    read_list(N, List), 
    find_min(List, Count),
    write("Min. element ", Count, " times"), nl.