// const StackArray = require('./StackArray');
const Stack = require('./Stack');

const stack = Stack;

test('Stack size test', () => {
    expect(stack.size()).toBe(0);
    stack.push(4, 8, 15, 16, 23, 42, {})
    expect(stack.size()).toBe(7);
    stack.pop()
    expect(stack.size()).toBe(6);
    stack.clear()
    expect(stack.size()).toBe(0);
});

test('Stack push test', () => {
    expect(stack.push(4, 8, {}, [1, 2, 3])).toEqual([4, 8, {}, [1, 2, 3]]);
    expect(stack.push(1, 2)).toEqual([1, 2]);
    expect(stack.push()).toEqual([]);
    stack.clear()
});

test('Stack pop test', () => {
    stack.push(4, 8, {}, [1, 2, 3], '{')
    expect(stack.pop()).toBe('{');
    stack.clear()
    expect(stack.pop()).toBeFalsy();
});

test('Stack clear test', () => {
    stack.push(4, 8, 15, 16, 23, 42)
    stack.clear()
    expect(stack.size()).toBe(0);
    expect(stack.clear()).toBe('clear');
});

test('Stack isEmpty test', () => {
    expect(stack.isEmpty()).toBeTruthy();
    stack.push([4, 8, 15, 16, 23, 42])
    expect(stack.isEmpty()).toBeFalsy();
});

test('Stack peek test', () => {
    stack.push(4, 8, 15)
    stack.pop()
    expect(stack.peek()).toBe(8);
    stack.push({name: "Andrey"})
    expect(stack.peek()).toEqual({name: "Andrey"});
    stack.clear()
    expect(stack.peek()).toBeFalsy();
    
});

test('Stack toArray test', () => {
    stack.push(4, 8, 15)
    stack.pop()
    expect(stack.toArray()).toEqual([4, 8]);
    stack.push({name: "Andrey"}, true, [])
    expect(stack.toArray()).toEqual([4, 8, {name: "Andrey"}, true, []]);
    stack.clear()
    expect(stack.toArray()).toEqual([]);
});