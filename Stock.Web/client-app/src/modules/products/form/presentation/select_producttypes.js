import React, {Component} from "react";
import api from "../../../../common/api";
import { Field } from "redux-form";
import SelectField from "../../../../components/inputs/SelectField";

class SelectProductTypes extends Component {
    _isMounted = false;
 
    /* constructor(props) {
      super(props);
   
      this.state = {
        news: [],
      };
    } */
    state = {
        options: []
    }

    componentDidMount(){
        this._isMounted = true;
        api.get('/productType')
        .then(response => {
            if (this._isMounted) {
                this.setState({
                    options: this.state.options.concat([{ value: "", label: 'Seleccione Categoría'}])
                });
                let a = response.data.map((each, index)=> {
                    return {
                    value : each.id,
                    label: each.description
                    }
                });
                this.setState({options:this.state.options.concat(a)});
            }
        })
        .catch(() => {
            return null;
        });
    }

    componentWillUnmount() {
        this._isMounted = false;
      }

    render() {    
    return (
        <Field label="Categorías" options={this.state.options} name="productTypeId" component={SelectField} type ="select"></Field>
    );
  }
}

export default SelectProductTypes;