export default function reverseFilter() {
    return function (items) {
        return items.slice().reverse();
    };
};