import React from "react";
import { Field } from "redux-form";
import api from "../../../../common/api";
import SelectField from "../../../../components/inputs/SelectField";

class InputList extends React.Component {
    constructor() {
    super();
    this.state = {
        options: []
    };
    }

    componentDidMount(){
        if(this.props.URL !== undefined){
            return api
            .get(this.props.URL)
            .then(response => {
                response.data.forEach(element => {
                    this.setState({
                        options: this.state.options.concat([{label: element[this.props.param], value: element.id}])
                    });
                });
            })
            .catch(() => {
                return null;
            });
        }
    }


    render() {    
    return (
        <Field label={this.props.label} options={this.state.options} name={this.props.name} component={SelectField}></Field>
    );
  }
}

export default InputList;