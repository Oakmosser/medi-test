export const TableRow = (obj, updateMethods) => 
{
    return(
        <tr>
            <td className="px-2 py-1">{obj.scan.id}</td>
            <td className="px-2 py-1">{obj.scan.dateCreated}</td>
            <td className="px-2 py-1">{obj.scan.patient}</td>
            <td className="px-2 py-1">
                <button onClick={() => updateMethods.fetchNotesTable(obj.scan.id)}>View Notes</button>
            </td>
        </tr>
    )
}