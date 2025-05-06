# Linked List GCD Inserter

Esse arquivo mostra a solução para o problema de dado uma lista []X composta por inteiros preencha as
lacunas dessa lista com o maximo divisor comum entre cada um dos nos da lista, exemplo:

##  Exemplo

Para entrada:
[18,6,10,3]

Deve ter a saida:
[18,6,6,2,10,1,3]

##  Problema

Given the head of a linked list head, in which each node contains an integer value.

Between every pair of adjacent nodes, insert a new node with a value equal to the greatest common divisor of them.

Return the linked list after insertion.

The greatest common divisor of two numbers is the largest positive integer that evenly divides both numbers.

1. Cria uma lista ligada com os valores fornecidos.
2. Insere um novo nó entre cada par de nós consecutivos contendo o **MDC** dos dois valores.
3. Exibe a lista resultante no console.

## Solution

## static void Main()
## {
##    int[] values = { 7 };
##
##    ListNode head = new ListNode() { Val = values[0] };
##    ListNode previous = head;
##
##    for (int index = 1; index < values.Length; index++)
##    {
##        ListNode node = new ListNode() { Val = values[index] };
##        previous.Next = node;
##        previous = node;
##    }
##
##    InsertGaps(head);
##
##    ShowList(head);
## }
##
## public static void ShowList(ListNode head)
## {
##    while (head != null)
##    {
##        Console.Write($"{head.Val} -> ");
##        head = head.Next;
##    }
## }
##
## public static void InsertGaps(ListNode head)
## {
##    while(head.Next != null)
##    {
##        int mdc = (int)BigInteger.GreatestCommonDivisor(head.Val, head.Next.Val);
##
##        ListNode newNode = new ListNode() { Val =  mdc, Next = head.Next };
##
##        ListNode nextIndex = head.Next;
##
##        head.Next = newNode;
##
##        head = nextIndex;
##    }
## }

## Novidade Utilizada?

Biblioteca System.Numerics para utilizar o método GreatestCommonDivisor e obter o maximo divisor comum de dois numeros