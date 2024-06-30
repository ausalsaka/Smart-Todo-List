using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MinHeap
{
    public List<(int priority, string value)> heap;

    public MinHeap()
    {
        heap = new List<(int priority, string value)>();
    }

    public void Insert(int priority, string value)
    {
        heap.Add((priority, value));
        HeapifyUp(heap.Count - 1);
    }

    public (int priority, string value) ExtractMin()
    {
        if (heap.Count == 0)
        {
            throw new InvalidOperationException("Heap is empty");
        }

        var min = heap[0];
        heap[0] = heap[heap.Count - 1];
        heap.RemoveAt(heap.Count - 1);
        HeapifyDown(0);
        return min;
    }
    public MinHeap Copy()
    {
        var copiedHeap = new MinHeap();
        copiedHeap.heap.AddRange(heap);
        return copiedHeap;
    }

    public (int priority, string value) PeekMin()
    {
        if (heap.Count == 0)
        {
            throw new InvalidOperationException("Heap is empty");
        }
        return heap[0];
    }

    public int Count => heap.Count;

    public List<(int priority, string value)> ToList()
    {
        return new List<(int priority, string value)>(heap);
    }

    private void HeapifyUp(int index)
    {
        while (index > 0)
        {
            int parentIndex = (index - 1) / 2;
            if (heap[index].priority >= heap[parentIndex].priority)
            {
                break;
            }

            Swap(index, parentIndex);
            index = parentIndex;
        }
    }

    public void Remove(int priority, string value)
    {
        Debug.Log("looking for " + value);
        //int index = heap.FindIndex(item => item.priority==priority && item.value == value);
        int index = heap.BinarySearch((priority, value));
        
        //index = heap.
        Debug.Log("found item at " + index);
        if (index == -1)
        {
            return;
        }
        

        heap[index] = heap[heap.Count - 1];
        heap.RemoveAt(heap.Count - 1);

        if (index < heap.Count)
        {
            heap.RemoveAt(index);
            HeapifyDown(index);
            HeapifyUp(index);
        }
    }

    private void HeapifyDown(int index)
    {
        int lastIndex = heap.Count - 1;
        while (index <= lastIndex)
        {
            int leftChildIndex = 2 * index + 1;
            int rightChildIndex = 2 * index + 2;
            int smallestIndex = index;

            if (leftChildIndex <= lastIndex && heap[leftChildIndex].priority < heap[smallestIndex].priority)
            {
                smallestIndex = leftChildIndex;
            }
            if (rightChildIndex <= lastIndex && heap[rightChildIndex].priority < heap[smallestIndex].priority)
            {
                smallestIndex = rightChildIndex;
            }
            if (smallestIndex == index)
            {
                break;
            }

            Swap(index, smallestIndex);
            index = smallestIndex;
        }
    }

    private void Swap(int index1, int index2)
    {
        var temp = heap[index1];
        heap[index1] = heap[index2];
        heap[index2] = temp;
    }
}