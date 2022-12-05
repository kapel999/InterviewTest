import React, { Component } from 'react';

import axios from 'axios';

export default class App extends Component {

    constructor() {
        super();
        this.state = {
            EmployeeData: [],
        }
    }

    componentDidMount() {
        axios.get("http://localhost:41478/Employees").then(response => {
            this.setState({
                EmployeeData: response.data
            });
        });
    }

    handleRemove(name) {
        axios.post('http://localhost:41478/List/DeleteRecord/' + name);
        window.location.reload(false);
    }

    handleEdit(event) {
        event.preventDefault();
        var oldName = event.target[0].value;
        var newName = event.target[1].value;
        var oldValue = event.target[2].value;
        var newValue = event.target[3].value;
        axios.post('http://localhost:41478/List/EditRecord/' + '?oldName=' + oldName + '&newName=' + newName + '&oldValue=' + oldValue + '&newValue=' + newValue);
        window.location.reload(false);
    }

    handleAdd(event) {
        event.preventDefault();
        var newName = event.target[0].value;
        var newValue = event.target[1].value;
        axios.post('http://localhost:41478/List/AddRecord/' + '?newName=' + newName + '&newValue=' + newValue);
        window.location.reload(false);
    }

    executeFirstQuery() {
        axios.post('http://localhost:41478/List/SQLQueryOne/');
        window.location.reload(false);
    }

    handleSecondQuery() {
        axios.post('http://localhost:41478/List/SQLQueryTwo/');
        window.location.reload(false);
    }

  render () {
    return (
        <section>
            <h1>Products List</h1>
            <div style={{ width: "30%", float: "left" }}>
                <div>
                    <table>
                        <thead><tr><th>Name</th><th>Value</th><th>Delete</th></tr></thead>
                        <tbody>
                            {
                                this.state.EmployeeData.map((p, index) => {
                                    return <tr key={index}><td>{p.name}</td><td>{p.value}</td><td><button type="button" onClick={() => this.handleRemove(p.name)}>Remove</button></td></tr>;
                                })
                            }
                        </tbody>
                    </table>
                </div>
            </div>

            <div>
                <h1>Add Product</h1>
                <form style={{ width: "500px" }} onSubmit={this.handleAdd}>
                    <label>
                        Name:
                        <input type="text" name="name" />
                        <br></br>
                        <br></br>
                        Value:
                        <input type="text" name="value" />
                    </label>
                    <br></br>
                    <br></br>
                    <input type="submit" value="Submit" style={{ marginLeft: "110px" }} />
                </form>
            </div>

            <div>
                <h1>Edit Product</h1>
                <form style={{ width: "500px" }} onSubmit={this.handleEdit}>
                    <label>
                        Name to Edit:
                        <input type="text" name="nameToEdit" />
                        <br></br>
                        <br></br>
                        New Name:
                        <input type="text" name="newName" />
                        <br></br>
                        <br></br>
                        Value to Edit:
                        <input type="text" name="valueToEdit" />
                        <br></br>
                        <br></br>
                        New Value:
                        <input type="text" name="newValue" />
                    </label>
                    <br></br>
                    <br></br>
                    <input type="submit" value="Submit" style={{ marginLeft: "110px" }} />
                </form>
            </div>

            <div>
                <h1>SQL query 1</h1>
                <button type="button" onClick={() => this.executeFirstQuery()}>Execute Query</button>
            </div>

            <div>
                <h1>SQL query 2</h1>
                <button type="button" onClick={() => this.handleSecondQuery()}>Execute Query</button>
            </div>
        </section>
    );
  }
}
