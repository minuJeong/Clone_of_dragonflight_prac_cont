﻿using UnityEngine;
using System.Collections;
using System.Collections.Generic;

/*
 * Usage
 * ObjectPool<GameObject> monster_pool; 
 * ...
 * // Create monsters.
 *  this.monster_pool = new ObjectPool<GameObject>(5, () =>  
    {
        GameObject obj = new GameObject("monster");
        return obj;
    });
 
    ...
    // Get from pool
    GameObject obj = this.monster_pool.pop();

    ...
    // Return to pool
    this.monster_pool.push(obj);
 * */  

public class ObjectPool<T> where T : class
{
    // Instance count to create
    short count;
    
    public delegate T Func();

    Func create_fn;
    
    // Instances.
    Stack<T> objects;
    
    // Construct
    public ObjectPool(short count, Func fn)
    {   
        this.count = count;
        this.create_fn = fn;
        this.objects = new Stack<T>(this.count);
        allocate();
    }
    
    void allocate()
    {
        for (int i=0; i<this.count; ++i)
        {   
            this.objects.Push(this.create_fn());
        }
    }
    
    public T pop()
    {
        if (this.objects.Count <= 0)
        {
            Debug.Log("Pool Expanding..");
            allocate();
        }

        return this.objects.Pop();
    }

    public void push(T obj)
    {
        this.objects.Push(obj);
    }  
}