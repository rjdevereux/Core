﻿// Copyright 2004-2011 Castle Project - http://www.castleproject.org/
// 
// Licensed under the Apache License, Version 2.0 (the "License");
// you may not use this file except in compliance with the License.
// You may obtain a copy of the License at
// 
//     http://www.apache.org/licenses/LICENSE-2.0
// 
// Unless required by applicable law or agreed to in writing, software
// distributed under the License is distributed on an "AS IS" BASIS,
// WITHOUT WARRANTIES OR CONDITIONS OF ANY KIND, either express or implied.f
// See the License for the specific language governing permissions and
// limitations under the License.

#if !SILVERLIGHT
namespace Castle.Components.DictionaryAdapter.Xml
{
	using System;
	using System.Collections;

	public class XmlComponentSerializer : XmlTypeSerializer
	{
		public static readonly XmlComponentSerializer
			Instance = new XmlComponentSerializer();

		protected XmlComponentSerializer() { }

		public override bool CanGetStub              { get { return true;  } }
		public override bool CanSerializeAsAttribute { get { return false; } }

		public override object GetStub(XmlIterator iterator, IDictionaryAdapter parent, IXmlAccessor accessor)
		{
            var adapter = new XmlAdapter(iterator);
			return CreateComponent(accessor.ClrType, adapter, parent);
		}

		public override object GetValue(XmlTypedNode node, IDictionaryAdapter parent, IXmlAccessor accessor)
		{
            var adapter = new XmlAdapter(node.Node);
			return CreateComponent(node.Type, adapter, parent);
		}

		public override void SetValue(XmlTypedNode node, IXmlAccessor accessor, object value)
		{
			throw new System.NotImplementedException();
		}

		public object CreateComponent(Type type, XmlAdapter adapter, IDictionaryAdapter parent)
		{
			var dictionary = new Hashtable();

			var descriptor = new DictionaryDescriptor(parent.Meta.Behaviors);
			parent.This.Descriptor.CopyBehaviors(descriptor);
			descriptor.AddBehavior(adapter);

			return parent.This.Factory.GetAdapter(type, dictionary, descriptor);
		}
	}
}
#endif
